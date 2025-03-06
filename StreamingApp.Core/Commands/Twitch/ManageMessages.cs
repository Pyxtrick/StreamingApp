using Microsoft.EntityFrameworkCore;
using StreamingApp.API.Interfaces;
using StreamingApp.API.Utility.Caching.Interface;
using StreamingApp.Core.Commands.DB.Interfaces;
using StreamingApp.Core.Commands.Hub;
using StreamingApp.Core.Commands.Twitch.Interfaces;
using StreamingApp.Core.Queries.Logic.Interfaces;
using StreamingApp.DB;
using StreamingApp.Domain.Entities.Dtos.Twitch;
using StreamingApp.Domain.Entities.Internal.Trigger;
using StreamingApp.Domain.Entities.Internal.User;
using StreamingApp.Domain.Enums;
using StreamingApp.Domain.Static;
using WebSocketSharp;

namespace StreamingApp.Core.Commands.Twitch;
public class ManageMessages : IManageMessages
{
    private readonly UnitOfWorkContext _unitOfWork;

    private readonly IAddUserToDB _addUserToDb;

    private readonly IUpdateUserOnDB _updateUserAchievementsOnDb;

    private readonly ITwitchCallCache _twitchCallCache;

    private readonly IEmotesCache _emotesCache;

    private readonly ISendSignalRMessage _sendSignalRMessage;

    private readonly ISendRequest _twitchSendRequest;

    private readonly IManageStream _manageStream;

    private readonly IManageCommands _manageCommands;

    private readonly IMessageCheck _messageCheck;

    private readonly IGameCommand _gameCommand;

    public ManageMessages(UnitOfWorkContext unitOfWork, IAddUserToDB addUserToDB, IUpdateUserOnDB updateUserAchievementsOnDb, ITwitchCallCache twitchCallCache, IEmotesCache emotesCache, ISendSignalRMessage sendSignalRMessage, ISendRequest twitchSendRequest, IManageStream manageStream, IManageCommands manageCommands, IMessageCheck messageCheck, IGameCommand gameCommand)
    {
        _unitOfWork = unitOfWork;
        _addUserToDb = addUserToDB;
        _updateUserAchievementsOnDb = updateUserAchievementsOnDb;
        _twitchCallCache = twitchCallCache;
        _emotesCache = emotesCache;
        _sendSignalRMessage = sendSignalRMessage;
        _twitchSendRequest = twitchSendRequest;
        _manageStream = manageStream;
        _manageCommands = manageCommands;
        _messageCheck = messageCheck;
        _gameCommand = gameCommand;
    }

    /// <summary>
    /// Get All Chached Twitch Messages from the past second
    /// </summary>
    /// <returns></returns>
    public async Task Execute()
    {
        // TODO: get Seconds from appsettings | _configuration["RefreshDelay:ChatRefresh"]
        List<object> value = _twitchCallCache.GetAllMessagesFromTo(DateTime.UtcNow.AddSeconds(-1), DateTime.UtcNow, CallCacheEnum.CachedMessageData);

        if (value.Count != 0)
        {
            List<MessageDto> messages = value.ConvertAll(s => (MessageDto)s);
            foreach (MessageDto message in messages)
            {
                await ExecuteOne(message);
            }
        }
    }

    public async Task ExecuteMultiple(List<MessageDto> messages)
    {
        foreach (var message in messages)
        {
            await ExecuteOne(message);
        }
    }

    public async Task ExecuteOne(MessageDto messageDto)
    {
        User user = _unitOfWork.User.Include("Ban").Include("Status").FirstOrDefault(u => u.TwitchDetail.UserName == messageDto.UserName);
        if (user != null)
        {
            // TODO: make backend check if this is the first message during the stream
            //messageDto.SpecialMessage.Add(SpecialMessgeEnum.FirstStreamMessage);

            await _updateUserAchievementsOnDb.UpdateAchievements(user.Id);
        }
        else
        {
            user = await _addUserToDb.AddUser(messageDto.UserId, messageDto.UserName, messageDto.IsSub, messageDto.SubCount, messageDto.Auth);
        }

        var isBroadcaster = messageDto.Auth.FirstOrDefault(e => e == AuthEnum.Streamer) == AuthEnum.Streamer;
        var isModerator = messageDto.Auth.FirstOrDefault(e => e == AuthEnum.Mod) == AuthEnum.Mod;
        var isStaff = messageDto.Auth.FirstOrDefault(e => e == AuthEnum.Staff) == AuthEnum.Staff;

        messageDto.Emotes.AddRange(MappBadges(messageDto));

        if (messageDto.Badges != null)
        {
            messageDto.Badges = MappBadges(messageDto.Badges);
        }
        else
        {
            messageDto.Badges = new();
        }
        string userId = messageDto.UserId;

        messageDto.Badges.Add(new(messageDto.Channel, messageDto.Channel.Contains("Pyxtrick")
            ? "https://static-cdn.jtvnw.net/jtv_user_pictures/f0eb150a-0f70-4876-977a-7eabb557fa79-profile_image-70x70.png"
            : $"https://static-cdn.jtvnw.net/jtv_user_pictures/{userId}-profile_image-70x70.png"));
        
        //var specialWords2 = await _unitOfWork.SpecialWords.FirstOrDefaultAsync();
        var s = _unitOfWork.SpecialWords.ToList();
        var newdata = await _unitOfWork.SpecialWords.ToListAsync();

        SpecialWords? specialWords = _unitOfWork.SpecialWords.FirstOrDefault(t => messageDto.Message.Contains(t.Name) && t.Type == SpecialWordEnum.Banned);
        
        // TODO: make a Class for this in API.Twitch
        // TODO: save to cache
        //GetCustomRewardsResponse rewardsResponse = await _twitchCache.GetTheTwitchAPI().Helix.ChannelPoints.GetCustomRewardAsync(_configuration["Twitch:ClientId"]);

        // Check Message befor it is sent to the Frontend or anywhere else
        if (await _messageCheck.Execute(messageDto, user) == false)
        {
            //SpecialWords? allowedMessage = _unitOfWork.SpecialWords.FirstOrDefault(t => t.Name.Contains(messageDto.Message) && t.Type == SpecialWordEnum.AllowedUrl);
            return;
        }

        

        

        // Check for beeing not an command or Rediam
        if (messageDto.IsCommand == false && messageDto.PointRediam.IsNullOrEmpty())
        {
            await MessageLogic(messageDto, user);
        }
        // Check if the Bot_OnChatCommandRecived is working as intended
        else if (messageDto.IsCommand && messageDto.PointRediam == null)
        {
            await CommandLogic(messageDto);
        }
        else if (messageDto.PointRediam != null)
        {
            await PointRedeamLogic(messageDto, user);
        }
    }

    private async Task MessageLogic(MessageDto messageDto, User user)
    {
        if (messageDto.Bits != 0)
        {
            List<Trigger> data = _unitOfWork.Trigger.Include("Targets").Include("TargetData").ToList();

            int found = 0;

            for (int i = 0; i < data.Count; i++)
            {
                data = ((List<Trigger>)data.Where(t => t.TriggerCondition == Domain.Enums.Trigger.TriggerCondition.Bits)).OrderBy(t => t.Ammount).ToList();

                if (messageDto.Bits == data[i].Ammount)
                {
                    var t = data[i].Targets.First(t => t.TargetCondition == Domain.Enums.Trigger.TargetCondition.Allert).TargetData;

                    //AlertDto alert = t.Alert;

                    // TODO: Show emote
                    //await _sendSignalRMessage.SendAllertAndEventMessage(user, messageDto, t);
                }
                else
                {
                    if (messageDto.Bits < data[i].Ammount)
                    {
                        if (data[i].Active && data[i].ExactAmmount == false)
                        {
                            found = i;
                        }
                    }
                    else if (messageDto.Bits > data[i].Ammount)
                    {
                        // TODO: Show emote
                    }
                }
            }
        }
        if (messageDto.Bits >= 200)
        {
            // TODO: Limit Message to a specific lenght
            // TODO: check for "Spam" or same thing in the message and cut it of at a certan time
            // TODO: TTS Message Play
            // TODO: able to skip TTS Message or Mute it
            // TODO: Show emote
        }

        await _sendSignalRMessage.SendChatMessage(user, messageDto);

        //_chatCache.AddOneChatData(messageDto);
    }

    private async Task CommandLogic(MessageDto messageDto)
    {
        string commandText = messageDto.Message.Split(' ').ToList()[0].Trim('!');

        CommandAndResponse? commandAndResponse = _unitOfWork.CommandAndResponse.FirstOrDefault(t => t.Command.Contains(commandText) && t.Active && messageDto.Auth.Min() >= t.Auth);

        if (commandAndResponse != null && commandAndResponse.Active == true && messageDto.Auth.First() <= commandAndResponse.Auth)
        {
            if (commandAndResponse.Category == CategoryEnum.Queue)
            {
                bool queueIsActive = _unitOfWork.Settings.FirstOrDefault().ComunityDayActive;

                if (queueIsActive)
                {
                    //_queueCommand.Execute(commandAndResponse, messageDto.Message, messageDto.UserName, messageDto.ChatOrigin);
                }

                Console.WriteLine("Command Queue");
            }
            else if (commandAndResponse.Category == CategoryEnum.Game)
            {
                _gameCommand.Execute(commandAndResponse);
                Console.WriteLine("Command Game");
            }
            // sstart, sstop, sset
            else if (commandAndResponse.Category == CategoryEnum.Subathon)
            {
                //commandAndResponse.Response.Replace("[SubathonTimer]", DateTime.Now.ToString());
                //_subathonCommand.Execute(commandAndResponse);
                Console.WriteLine("Command Subathon");
            }
            // Fun     flip,random,rainbow,revert,bounce,random,translatehell,gigantify
            else if (commandAndResponse.Category == CategoryEnum.Fun)
            {
                //_ManageFun.Execute(commandAndResponse);
            }
            else if (commandAndResponse.HasLogic)
            {
                await _manageCommands.Execute(messageDto, commandAndResponse);
                Console.WriteLine("Command Has Logic");
            }
            else if (commandAndResponse.Category == CategoryEnum.StreamUpdate)
            {
                await _manageStream.Execute(messageDto);
            }
            else
            {
                // TODO: Replace parts in the message
                string message = commandAndResponse.Response;

                var stream = _unitOfWork.StreamHistory.OrderBy(s => s.StreamStart).Last();

                message.Replace("[User]", messageDto.UserName);
                message.Replace("[Time]", DateTime.Now.ToString());
                message.Replace("[StreamStartTime]", stream.StreamStart.ToString());
                message.Replace("[StreamLiveTime]", DateTime.UtcNow.Subtract(stream.StreamStart).ToString());

                _twitchSendRequest.SendChatMessage(commandAndResponse.Response);
            }
        }
    }

    private async Task PointRedeamLogic(MessageDto messageDto, User user)
    {
        Console.WriteLine(messageDto);

        // TODO: make a Class for this in API.Twitch
        //GetCustomRewardsResponse rewardsResponse = await _twitchCache.GetTheTwitchAPI().Helix.ChannelPoints.GetCustomRewardAsync(_configuration["Twitch:ClientId"]);

        // Use EmotesCondition.ChannelPoints
        // TODO: Check id for rediam 
        // TODO: Do what ever the rediam is

        //await _sendSignalRMessage.SendAllertAndEventMessage(data, user, messageDto);
    }

    private List<KeyValuePair<string, string>> MappBadges(List<KeyValuePair<string, string>> userBadges)
    {
        List<KeyValuePair<string, string>> badges = new();

        foreach (var userBadge in userBadges)
        {
            // TODO: Get Badges form DB
            var allBadges = BadgesData.GetAllBadges();

            var badge = allBadges.FirstOrDefault(b => b.Value == userBadge.Key);

            if (badge.Value != null)
            {
                badges.Add(new(badge.Value, $"https://static-cdn.jtvnw.net/badges/v1/{badge.Key}/1"));
            }
        }

        return badges;
    }

    private List<EmoteSet>? MappBadges(MessageDto messageDto)
    {
        var emotes = _emotesCache.GetEmotes(null);

        var foundEmotes = emotes.Where(e => messageDto.Message.Contains(e.Name)).ToList();

        List<EmoteSet> emoteList = new();

        if (foundEmotes.Any())
        {
            foreach (var emote in foundEmotes)
            {
                emoteList.Add(new EmoteSet() { Name = emote.Name, AnimatedURL = emote.Url, StaticURL = emote.Url });
            }
        }

        return emoteList;

    }
}
