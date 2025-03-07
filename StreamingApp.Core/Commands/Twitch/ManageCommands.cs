using Microsoft.EntityFrameworkCore;
using StreamingApp.API.BetterTV_7TV;
using StreamingApp.API.Interfaces;
using StreamingApp.Core.Commands.Twitch.Interfaces;
using StreamingApp.DB;
using StreamingApp.Domain.Entities.Dtos.Twitch;
using StreamingApp.Domain.Entities.Internal.Trigger;
using StreamingApp.Domain.Entities.Internal.User;
using StreamingApp.Domain.Enums;
using WebSocketSharp;

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
        string[] splitNoCommandTextMessage = new List<string>().ToArray();

        if (splitMessage.Length > 1)
        {
            if (splitMessage[1].Contains("\U000e0000")) {
                splitMessage = splitMessage.Where(s => s == splitMessage[1]).ToArray();
            }
            splitNoCommandTextMessage = messageDto.Message[(messageDto.Message.Split()[0].Length + 1)..].Split(' ');
        }

        if (commandAndResponse != null && commandAndResponse.Active == true)
        {
            string response = "";

            bool reply = false;

            // Update / Refresh Emotes from 7tv and Betterttv
            
            if(splitMessage[0].Equals("!timer"))
            {
                // TODO: create Timer for x seconds or minutes
                // Countdown
            }
            else if(splitMessage[0].Equals("!tracker"))
            {
                if(splitNoCommandTextMessage.Length > 0)
                {
                    var specialWord = await _unitOfWork.SpecialWords.FirstOrDefaultAsync(s => s.Name.Contains(splitNoCommandTextMessage[0]) && s.Type == Domain.Enums.SpecialWordEnum.Count);

                    if (specialWord != null)
                    {
                        response = $"{splitNoCommandTextMessage[0]} has been used {specialWord.TimesUsed} Times";
                    }
                }
            }
            // response with the time when stream goes live / with conversion to other time zones
            else if(splitMessage[0].Equals("!live"))
            {
                var timeTexts = _unitOfWork.CommandAndResponse.FirstOrDefault(c => c.Command.Equals("streamTime")).Response.Split(",").ToList();

                var streamTimes = new List<DateTime>();

                foreach (var timeText in timeTexts)
                {
                    var split = timeText.Split(" ");

                    var localTime = DateTime.Parse($"{split[1]} {split[2]}");

                    streamTimes.Add(localTime.AddDays(((int)(DayOfWeek)Enum.Parse(typeof(DayOfWeek), split[1]) - (int)localTime.DayOfWeek + 7) % 7));
                }

                var result = streamTimes.Order().First();

                //https://en.wikipedia.org/wiki/List_of_tz_database_time_zones
                var timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(splitNoCommandTextMessage.Length > 0 ? splitNoCommandTextMessage[0] : "Europe/Zurich");

                int pFrom = timeZoneInfo.DisplayName.IndexOf("(") + "(".Length;
                int pTo = timeZoneInfo.DisplayName.LastIndexOf(")");
                string offset = timeZoneInfo.DisplayName.Substring(pFrom, pTo - pFrom);

                if (splitNoCommandTextMessage.Length > 0)
                {
                    try
                    {
                        var timeZone = TimeZoneInfo.ConvertTime(result, TimeZoneInfo.Local, TimeZoneInfo.FindSystemTimeZoneById(splitNoCommandTextMessage[0]));

                        response = $"Stream will be live on {timeZone.DayOfWeek} and {timeZone.AddDays(1).DayOfWeek} at {timeZone.TimeOfDay.ToString()} {splitNoCommandTextMessage[1]} / {offset}";
                    }
                    catch (Exception e)
                    {
                        response = commandAndResponse.Response;
                    }
                }
                else
                {
                    response = $"Stream will be live on {result.DayOfWeek} and {result.AddDays(1).DayOfWeek} at {result.TimeOfDay.ToString()} CET / {offset}";
                }
            }
            else if(splitMessage[0].Equals("!currentTime"))
            {
                var localTime = DateTime.Now.Date + new TimeSpan(10, 00, 0);

                if (splitNoCommandTextMessage.Length > 1)
                {
                    var timeZone = TimeZoneInfo.ConvertTime(localTime, TimeZoneInfo.Local, TimeZoneInfo.FindSystemTimeZoneById(splitNoCommandTextMessage[0]));

                    var offset = (timeZone - localTime.ToUniversalTime()).Hours;

                    var offsetText = offset > 0 ? $"+{offset}" : $"{offset}";

                    response = $"It is Currenty in {splitNoCommandTextMessage[0]} {timeZone.ToShortTimeString} (UTC {offsetText})";
                }
                else
                {
                    response = $"It is Currenty {localTime.ToShortTimeString}";
                }
            }
            else if(splitMessage[0].Equals("!language2"))
            {
                // Use Twitch user Language and send message in chat
            }
            else if(splitMessage[0].Equals("!clip2"))
            {
                // lgic to create a OBS Clip / checkpoint
            }
            else if(splitMessage[0].Equals("!song"))
            {
                // reads the current song application / API to get the currenty plaing song
            }
            else if(splitMessage[0].Equals("!collab"))
            {
                if (!commandAndResponse.Active)
                {
                    // No collab
                    response = commandAndResponse.Response;

                    return;
                }

                string peopleLinks = "";

                // get all collab peole from the message
                foreach (var text in splitNoCommandTextMessage)
                {
                    var user = await _unitOfWork.UserDetail.FirstAsync(x => x.UserName == text);

                    if(user != null)
                    {
                        string userLink = user.Url != null ? user.Url : $"https://www.twitch.tv/{user.UserName}";

                        peopleLinks += $"{user.UserName}: {user.Url} ";
                    }
                    else
                    {
                        Console.WriteLine($"User {text} is not in the DB yet");

                        peopleLinks += $"{text}: https://www.twitch.tv/{text} ";
                    }
                }

                response = $"Current Collab with {peopleLinks}";
            }
            else if(splitMessage[0].Equals("!statistics"))
            {
                User user = _unitOfWork.User.Include("Ban").Include("Status").FirstOrDefault(u => u.TwitchDetail.UserName == messageDto.UserName);

                DateTime FirstStreamSeen = user.TwitchAchievements.FirstStreamSeen;
                int streamStreak = user.TwitchAchievements.WachedStreams;

                response = $"User {messageDto.UserName} has seen {streamStreak} since {FirstStreamSeen.ToShortDateString()}";
            }
            else if (messageDto.Auth.Min() <= AuthEnum.Mod)
            {
                if (splitMessage[0].Equals("!permit"))
                {
                    // TODO: Permit user to send a Link for 2 Minutes
                }
                else if (splitMessage[0].Equals("!randomuser"))
                {
                    // chooses a random user who has chatted in the current stream (Achievements.LastStreamSeen)
                    // Use TwitchCallCache.GetAllMessages
                    // var t = _twitchCallCache.GetAllMessages(CallCacheEnum.CachedMessageData);
                    //List<MessageDto> messages = t.ConvertAll(s => (MessageDto)s);
                }
                else if (splitMessage[0].Equals("!updateEmotes"))
                {
                    await _emotesApiRequest.GetTVEmoteSet();

                    response = "Emotes have been updated";
                }
            }

            if (!response.IsNullOrEmpty())
            {
                if (reply)
                {
                    _twitchSendRequest.SendResplyChatMessage(response, messageDto.MessageId);
                }
                else
                {
                    _twitchSendRequest.SendChatMessage(response);
                }
            }
        }
    }
}
