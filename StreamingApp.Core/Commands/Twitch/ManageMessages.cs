using Microsoft.EntityFrameworkCore;
using StreamingApp.API.Utility.Caching.Interface;
using StreamingApp.Core.Commands.DB;
using StreamingApp.Core.Commands.Twitch.Interfaces;
using StreamingApp.DB;
using StreamingApp.Domain.Entities.Dtos.Twitch;
using StreamingApp.Domain.Entities.Internal;
using StreamingApp.Domain.Enums;

namespace StreamingApp.Core.Commands.Twitch;
public class ManageMessages : IManageMessages
{
    private readonly UnitOfWorkContext _unitOfWork;

    private readonly IAddUserToDB _addUserToDb;

    private readonly IUpdateUserAchievementsOnDB _updateUserAchievementsOnDb;

    private readonly ITwitchCallCache _twitchCallCache;

    public ManageMessages(UnitOfWorkContext unitOfWork, IAddUserToDB addUserToDB, IUpdateUserAchievementsOnDB updateUserAchievementsOnDb, ITwitchCallCache twitchCallCache)
    {
        _unitOfWork = unitOfWork;
        _addUserToDb = addUserToDB;
        _updateUserAchievementsOnDb = updateUserAchievementsOnDb;
        _twitchCallCache = twitchCallCache;
    }

    public async Task Execute()
    {
        //TODO: Check if 1 second is enouth for this
        IList<MessageDto> t = new List<MessageDto>(); // (IList<MessageDto>)_twitchCallCache.GetAllMessagesFromTo(DateTime.UtcNow.AddSeconds(-1), DateTime.UtcNow, CallCacheEnum.CachedMessageData);

        if (t != null)
        {
            foreach (var item in t)
            {
                Console.WriteLine(item.Message);
                //TODO: Uncomments this
                //await Execute(item);
            }
        }
    }

    public async Task Execute(MessageDto messageDto)
    {
        var isBroadcaster = messageDto.Auth.FirstOrDefault(e => e == AuthEnum.Streamer) == AuthEnum.Streamer;
        var isModerator = messageDto.Auth.FirstOrDefault(e => e == AuthEnum.Mod) == AuthEnum.Mod;
        var isStaff = messageDto.Auth.FirstOrDefault(e => e == AuthEnum.Staff) == AuthEnum.Staff;

        //var specialWords2 = await _unitOfWork.SpecialWords.FirstOrDefaultAsync();
        var s = _unitOfWork.SpecialWords.ToList();
        var newdata = await _unitOfWork.SpecialWords.ToListAsync();

        SpecialWords? specialWords = _unitOfWork.SpecialWords.First(t => t.Name.Contains(messageDto.Message) && t.Type == SpecialWordEnum.Banned);

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

        var t = _unitOfWork.User.FirstOrDefault(u => u.TwitchAchievements.LastStreamSeen > DateTime.Now.AddHours(24));
        if (t != null)
        {
            // TODO: make backend check if this is the first message during the stream
            messageDto.SpecialMessage.Add(SpecialMessgeEnum.FirstStreamMessage);
        }

        // Check for beeing an command / Rediam
        if (!messageDto.Message[0].ToString().Contains("!") && messageDto.PointRediam == null)
        {
            if (messageDto.Bits != 0)
            {
                List<EmotesCondition> data = _unitOfWork.EmotesCondition.ToList();

                int found = 0;

                for (int i = 0; i < data.Count; i++)
                {
                    if (messageDto.Bits == data[i].BitAmmount)
                    {
                        // TODO: Show emote
                    }
                    else
                    {

                        if (messageDto.Bits < data[i].BitAmmount)
                        {
                            if (data[i].Active && data[i].ExactAmmount == false)
                            {
                                found = i;
                            }
                        }
                        else if (messageDto.Bits > data[i].BitAmmount)
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

            //_chatCache.AddOneChatData(messageDto);
        }
        /** Check if the Bot_OnChatCommandRecived is working as intended
        else if (!message[0].ToString().Contains("!") && pointRediam == null)
        {
            string commandText = message.Split(' ').ToList()[0].Trim('!');

            List<AuthEnum> authEnums = (from auth in auths select (AuthEnum)Enum.Parse(typeof(AuthEnum), auth.ToString())).ToList();

            CommandAndResponse? commandAndResponse = CommandsStaticResponse.FirstOrDefault(t => t.Command.Contains(commandText) && t.Active && authEnums.Contains(t.Auth));


            if (commandAndResponse != null && commandAndResponse.Active == true)
            {
                if (commandAndResponse.Category == CategoryEnum.Queue)
                {
                    _queueCommand.Execute(commandAndResponse, message, userName);
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
        }
    }
}
