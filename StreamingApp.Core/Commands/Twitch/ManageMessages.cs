using Microsoft.EntityFrameworkCore;
using StreamingApp.API.Utility.Caching.Interface;
using StreamingApp.Core.Commands.Chat;
using StreamingApp.Core.Commands.DB.Interfaces;
using StreamingApp.Core.Commands.Twitch.Interfaces;
using StreamingApp.DB;
using StreamingApp.Domain.Entities.Dtos.Twitch;
using StreamingApp.Domain.Entities.Internal.Trigger;
using StreamingApp.Domain.Entities.Internal.User;
using StreamingApp.Domain.Enums;

namespace StreamingApp.Core.Commands.Twitch;
public class ManageMessages : IManageMessages
{
    private readonly UnitOfWorkContext _unitOfWork;

    private readonly IAddUserToDB _addUserToDb;

    private readonly IUpdateUserAchievementsOnDB _updateUserAchievementsOnDb;

    private readonly ITwitchCallCache _twitchCallCache;

    private readonly ISendSignalRMessage _sendSignalRMessage;

    public ManageMessages(UnitOfWorkContext unitOfWork, IAddUserToDB addUserToDB, IUpdateUserAchievementsOnDB updateUserAchievementsOnDb, ITwitchCallCache twitchCallCache, ISendSignalRMessage sendSignalRMessage)
    {
        _unitOfWork = unitOfWork;
        _addUserToDb = addUserToDB;
        _updateUserAchievementsOnDb = updateUserAchievementsOnDb;
        _twitchCallCache = twitchCallCache;
        _sendSignalRMessage = sendSignalRMessage;
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
        var isBroadcaster = messageDto.Auth.FirstOrDefault(e => e == AuthEnum.Streamer) == AuthEnum.Streamer;
        var isModerator = messageDto.Auth.FirstOrDefault(e => e == AuthEnum.Mod) == AuthEnum.Mod;
        var isStaff = messageDto.Auth.FirstOrDefault(e => e == AuthEnum.Staff) == AuthEnum.Staff;

        //var specialWords2 = await _unitOfWork.SpecialWords.FirstOrDefaultAsync();
        var s = _unitOfWork.SpecialWords.ToList();
        var newdata = await _unitOfWork.SpecialWords.ToListAsync();

        SpecialWords? specialWords = _unitOfWork.SpecialWords.FirstOrDefault(t => messageDto.Message.Contains(t.Name) && t.Type == SpecialWordEnum.Banned);

        // if it is a banned word and not the Broadcaster, Moderator or Staff
        if (specialWords != null && !isBroadcaster && !isModerator && !isStaff)
        {
            // TODO: make a Class for this in API.Twitch
            //GetCustomRewardsResponse rewardsResponse = await _twitchCache.GetTheTwitchAPI().Helix.ChannelPoints.GetCustomRewardAsync(_configuration["Twitch:ClientId"]);

            SpecialWords? allowedMessage = _unitOfWork.SpecialWords.FirstOrDefault(t => t.Name.Contains(messageDto.Message) && t.Type == SpecialWordEnum.Allowed);

            if (allowedMessage != null)
            {
                // Delete Message
                // TODO: make a Class for this in API.Twitch
                //await _twitchCache.GetTheTwitchAPI().Helix.Moderation.DeleteChatMessagesAsync("Pyxtrick", "PyxtrickBot", e.ChatMessage.Id);

                //Tiemout User for 60 seconds
                //await _twitchCache.GetTheTwitchAPI().Helix.Moderation.BanUserAsync("", "", new TwitchLib.Api.Helix.Models.Moderation.BanUser.BanUserRequest() { Duration = 60, Reason = $"Used Internaly Banded Word {allowedMessage.Name}", UserId = e.ChatMessage.UserId });

                // TODO: time out the user / delete message
                // TODO: maybe have a message displayed / like @User do not send url's in the chat

                return;
            }
        }

        var internalUserId = await _addUserToDb.AddUser(messageDto.UserId, messageDto.UserName, messageDto.IsSub, messageDto.SubCount, messageDto.Auth);
        await _updateUserAchievementsOnDb.UpdateAchievements(internalUserId);

        User user = _unitOfWork.User.FirstOrDefault(u => u.TwitchDetail.UserName == messageDto.UserName);

        {
            // TODO: make backend check if this is the first message during the stream
            messageDto.SpecialMessage.Add(SpecialMessgeEnum.FirstStreamMessage);
        }

        // Check for beeing an command / Rediam
        if (!messageDto.Message[0].ToString().Contains("!") && messageDto.PointRediam == null)
        {
            if (messageDto.Bits != 0)
            {
                List<Trigger> data = _unitOfWork.Trigger.ToList();

                int found = 0;

                for (int i = 0; i < data.Count; i++)
                {
                    data = ((List<Trigger>)data.Where(t => t.TriggerCondition == Domain.Enums.Trigger.TriggerCondition.Bits)).OrderBy(t => t.Ammount).ToList();

                    if (messageDto.Bits == data[i].Ammount)
                    {
                        // TODO: Show emote
                        //await _sendSignalRMessage.SendAllertAndEventMessage(data, user, messageDto);
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
        // Check if the Bot_OnChatCommandRecived is working as intended
        /**else if (!messageDto.Message[0].ToString().Contains("!"))// && pointRediam == null)
        {
            string commandText = messageDto.Message.Split(' ').ToList()[0].Trim('!');

            List<AuthEnum> authEnums = (from auth in auths select (AuthEnum)Enum.Parse(typeof(AuthEnum), auth.ToString())).ToList();

            CommandAndResponse? commandAndResponse = CommandsStaticResponse.FirstOrDefault(t => t.Command.Contains(commandText) && t.Active && authEnums.Contains(t.Auth));


            if (commandAndResponse != null && commandAndResponse.Active == true)
            {
                if (commandAndResponse.Category == CategoryEnum.Queue)
                {
                    _queueCommand.Execute(commandAndResponse, messageDto.Message, messageDto.UserName);
                }
                else if (commandAndResponse.Category == CategoryEnum.Game)
                {
                    _gameCommand.Execute(commandAndResponse);
                }
                else
                {
                    if (CheckIfAvalibleToUse(message, authEnums)) {
                        _twitchCache.GetOwnerOfChannelConnection().SendMessage(_twitchCache.GetTwitchChannelName(), commandAndResponse.Response);
                    }
                }
            }
        }**/

        // TODO: hydrate
        else if (messageDto.PointRediam != null)
        {
            // TODO: make a Class for this in API.Twitch
            //GetCustomRewardsResponse rewardsResponse = await _twitchCache.GetTheTwitchAPI().Helix.ChannelPoints.GetCustomRewardAsync(_configuration["Twitch:ClientId"]);

            // Use EmotesCondition.ChannelPoints
            // TODO: Check id for rediam 
            // TODO: Do what ever the rediam is

            //await _sendSignalRMessage.SendAllertAndEventMessage(data, user, messageDto);
        }
    }
}
