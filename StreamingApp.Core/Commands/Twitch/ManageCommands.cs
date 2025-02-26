using Microsoft.EntityFrameworkCore;
using StreamingApp.API.BetterTV_7TV;
using StreamingApp.API.Interfaces;
using StreamingApp.API.Utility.Caching.Interface;
using StreamingApp.Core.Commands.Twitch.Interfaces;
using StreamingApp.DB;
using StreamingApp.Domain.Entities.Dtos.Twitch;
using StreamingApp.Domain.Entities.Internal.Trigger;
using StreamingApp.Domain.Entities.Internal.User;

namespace StreamingApp.Core.Commands.Twitch;
public class ManageCommands : IManageCommands
{
    private readonly UnitOfWorkContext _unitOfWork;

    private readonly ISendRequest _twitchSendRequest;

    private readonly IEmotesApiRequest _emotesApiRequest;

    public ManageCommands(UnitOfWorkContext unitOfWork, ISendRequest sendRequest, IEmotesApiRequest emotesApiRequest)
    {
        _unitOfWork = unitOfWork;
        _twitchSendRequest = sendRequest;
        _emotesApiRequest = emotesApiRequest;
    }

    public async Task Execute(MessageDto messageDto, CommandAndResponse commandAndResponse)
    {
        var splitMessage = messageDto.Message.Split(' ');

        if (commandAndResponse != null && commandAndResponse.Active == true)
        {
            // Update / Refresh Emotes from 7tv and Betterttv
            if (commandAndResponse.Command.Contains("update"))
            {
                await _emotesApiRequest.GetTVEmoteSet();
            }
            else if (commandAndResponse.Command.Contains("timer"))
            {
                // TODO: create Timer for x seconds or minutes
                // Countdown
            }
            // response with the time when stream goes live / with conversion to other time zones
            else if (commandAndResponse.Command.Contains("live"))
            {
                var localTime = DateTime.Now.Date + new TimeSpan(10, 00, 0);
                string response = "undefined";

                if (splitMessage.Length > 1)
                {
                    try
                    {
                        var result = localTime.AddDays(((int)DayOfWeek.Friday - (int)localTime.DayOfWeek + 7) % 7);

                        var timeZone = TimeZoneInfo.ConvertTime(result, TimeZoneInfo.Local, TimeZoneInfo.FindSystemTimeZoneById(splitMessage[2]));

                        var offset = (timeZone - result.ToUniversalTime()).Hours;

                        var offsetText = offset > 0 ? $"+{offset}" : $"{offset}";

                        response = $"Stream will be live on {timeZone.DayOfWeek} and {timeZone.AddDays(1).DayOfWeek} at {timeZone.TimeOfDay.ToString()} {splitMessage[2]} / (UTC {offsetText})";
                    }
                    catch (Exception)
                    {
                        response = commandAndResponse.Response;
                    }
                }
                else
                {
                    var result = localTime.AddDays(((int)DayOfWeek.Friday - (int)localTime.DayOfWeek + 7) % 7);

                    response = $"Stream will be live on {result.DayOfWeek} and {result.AddDays(1).DayOfWeek} at {localTime.TimeOfDay.ToString()} {result.Kind} / {result.ToUniversalTime().TimeOfDay} {result.ToUniversalTime().Kind}";
                }

                // TODO: make a Class for this in API.Twitch
                _twitchSendRequest.SendChatMessage(response);
            }
            else if (commandAndResponse.Command.Contains("language2"))
            {
                // Use Twitch user Language and send message in chat
            }
            else if (commandAndResponse.Command.Contains("clip2"))
            {
                // lgic to create a OBS Clip / checkpoint
            }
            else if (commandAndResponse.Command.Contains("song"))
            {
                // reads the current song application / API to get the currenty plaing song
            }
            else if (commandAndResponse.Command.Contains("collab"))
            {
                string response = "";

                if (!commandAndResponse.Active)
                {
                    // No collab
                    response = commandAndResponse.Response;

                    return;
                }

                string peopleLinks = "";

                // get all collab peole from the message
                foreach (var command in splitMessage)
                {
                    // Get DB enty of links for the collab people
                    peopleLinks += "userLink";
                }

                response = $"Current Collab with {peopleLinks}";
            }
            else if (commandAndResponse.Command.Contains("statistics"))
            {
                User user = _unitOfWork.User.Include("Ban").Include("Status").FirstOrDefault(u => u.TwitchDetail.UserName == messageDto.UserName);

                DateTime FirstStreamSeen = user.TwitchAchievements.FirstStreamSeen;
                int streamStreak = user.TwitchAchievements.WachedStreams;

                var response = $"User {messageDto.UserName} has seen {streamStreak} since {FirstStreamSeen.ToShortDateString()}";

                _twitchSendRequest.SendChatMessage(response);
            }
            else if (commandAndResponse.Command.Contains("randomuser"))
            {
                // chooses a random user who has chatted in the current stream (Achievements.LastStreamSeen)
                // Use TwitchCallCache.GetAllMessages
                // var t = _twitchCallCache.GetAllMessages(CallCacheEnum.CachedMessageData);
                //List<MessageDto> messages = t.ConvertAll(s => (MessageDto)s);
            }
        }
    }
}
