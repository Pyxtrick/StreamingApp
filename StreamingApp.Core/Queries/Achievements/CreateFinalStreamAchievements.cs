using Microsoft.EntityFrameworkCore;
using StreamingApp.API.Utility.Caching.Interface;
using StreamingApp.Core.Commands.FileLogic;
using StreamingApp.DB;
using StreamingApp.Domain.Enums;
using System.Text;

namespace StreamingApp.Core.Queries.Achievements;

public class CreateFinalStreamAchievements : ICreateFinalStreamAchievements
{
    private readonly ITwitchCallCache _twitchCallCache;

    private readonly IManageFile _manageFile;

    private UnitOfWorkContext _unitOfWork;

    public CreateFinalStreamAchievements(ITwitchCallCache twitchCallCache, IManageFile manageFile, UnitOfWorkContext unitOfWorkContext)
    {
        _twitchCallCache = twitchCallCache;
        _manageFile = manageFile;
        _unitOfWork = unitOfWorkContext;
    }

    public async Task<string> Execute()
    {
        var stream = _unitOfWork.StreamHistory.OrderBy(x => x.Id).Last();

        var streamChattedViewers = _unitOfWork.User.Include("Achievements").Include("Ban").Where(u => u.Achievements.FirstOrDefault(t => t.Origin == OriginEnum.Twitch).LastStreamSeen >= stream.StreamStart && u.Achievements.FirstOrDefault(t => t.Origin == OriginEnum.Twitch).LastStreamSeen <= DateTime.UtcNow);

        var newViewers = streamChattedViewers.Where(u => u.Achievements.FirstOrDefault(t => t.Origin == OriginEnum.Twitch).WachedStreams == 1 && u.Ban.IsBaned == false);

        var duration = DateTime.Now - stream.StreamStart.AddHours(1);
        var days = duration.Days > 0 ? $"{duration.Days.ToString()} Days, " : "";

        var messageCount = _twitchCallCache.GetChachedNumberCount(CallCacheEnum.CachedMessageData);
        var subCount = _twitchCallCache.GetChachedNumberCount(CallCacheEnum.CachedSubData);
        var raidCount = _twitchCallCache.GetChachedNumberCount(CallCacheEnum.CachedRaidData);
        var raidUserCount = _twitchCallCache.GetChachedNumberCount(CallCacheEnum.CachedRaidUserData);
        var bannedCount = _twitchCallCache.GetChachedNumberCount(CallCacheEnum.CachedBannedData);

        List<string> twitchAchievements = new List<string>();

        /** TODO: Fix _manageFile
        var fileText = _manageFile.ReadFile();

        foreach (var item in fileText)
        {
            string[] parts = item.Split('-');

            if (parts.Length == 4)
            {
                if (parts[0].Contains(nameof(ChatOriginEnum.Twitch)))
                {
                    twitchAchievements.Add($"User: {parts[1]} has gifted {parts[3]} {parts[2]}");
                }
            }
        }**/

        if (newViewers.Any())
        {
            foreach (var viewer in newViewers)
            {
                twitchAchievements.Add($"{viewer.UserText} has First Chatted at {viewer.Achievements.FirstOrDefault(t => t.Origin == OriginEnum.Twitch).LastStreamSeen.ToLocalTime().ToShortTimeString()}");
            }
        }

        var streamTimes = (await _unitOfWork.CommandAndResponse.FirstAsync(c => c.Command.Equals("streamtime"))).Response.Split(",").ToList();
        var streamDuration = $"Stream was from {stream.StreamStart.AddHours(1).ToString("dd.MM.yyyy HH:mm")} to {DateTime.Now.ToString("dd.MM.yyyy HH:mm")} with a duration of {days}{duration.Hours} Hours and {duration.Minutes} Minutes";
        var messageText = messageCount != 0 ? $"Messages Recived: {messageCount} Sent by {streamChattedViewers.Count()} Users" : "";
        var newViewerText = newViewers.Count() != 0 ? $"{newViewers.Count()} New Viewers have Chatted" : "";
        var subText = subCount != 0 ? $"Subs Recived: {subCount}" : "";
        var raidText = raidCount != 0 ? $"{raidCount} Raids with {raidUserCount} Users" : "";

        var lines = new List<string>() { "Final Stream Achievemenst", streamDuration, messageText, subText, raidText };

        // TODO: add back when _manageFile is Fixed
        //_manageFile.WriteFile(lines.ToArray(), true);

        return await CreateHdmlFile("ScrollText", streamDuration, messageText, newViewerText, subText, raidText, twitchAchievements, streamTimes);
    }

    private async Task<string> CreateHdmlFile(string alertName, string streamDuration, string messageText, string newViewerText, string subText, string raidText, List<string> twitchAchievements, List<string> streamTimes)
    {
        var alert = await _unitOfWork.Alert.FirstAsync(a => a.Name.Equals(alertName));

        // Decode
        var html = Encoding.ASCII.GetString(alert.Html);

        html = html.Replace("[StreamDuration]", streamDuration);
        html = html.Replace("[MessageText]", messageText);
        html = html.Replace("[NewViewerText]", newViewerText);
        html = html.Replace("[SubText]", subText);
        html = html.Replace("[RaidText]", raidText);
        html = html.Replace("[UserText]", twitchAchievements.Count() != 0 ? $"{string.Join("</a><a>", twitchAchievements)}" : "");
        html = html.Replace("[StreamTimes]", streamTimes.Count() != 0 ? $"{string.Join("</div><div>", streamTimes)}" : "");

        //TODO: use this when ScrollText is in the Alert DB Table
        //return text;

        // Encode
        //Encoding.ASCII.GetBytes(t)

        // TODO: Save in backend as byte[]
        var userText = twitchAchievements.Count() != 0 ? $"{string.Join("</a><a>", twitchAchievements)}" : "";
        return "<html lang=\"en\">\r\n <body>" +
            "<div id=\"target\">" +
                "<div>" +
                    "<div class=\"stats\">Stream Stats</div>" +
                    $"<div>{streamDuration}</div>" +
                    $"<div>{messageText}</div>" +
                    $"<div>{newViewerText}</div>" +
                    $"<div>{subText}</div>" +
                    $"<div>{raidText}</div>" +
                    "<div class=\"user-stats\">User Stats</div>" +
                    $"<div>{userText}</div>" +
                    "<div class=\"user-stats\">Next Streams</div>" +
                    "<div>Tuesday 9 PM CEST (UTC +2)</div>" +
                    "<div>Wednesday 9 PM CEST (UTC +2)</div>" +
                    "<div>Friday 9 PM CEST (UTC +2)</div>" +
                    "<div class=\"stats\">Stream End</div>" +
                    "<div>Thanks for Watching</div>" +
                "</div>" +
            "</div>" +
            "</body>" +
            "<style>" +
                "#target {\r\n      position: absolute;\r\n      top: -2300px;\r\n      bottom: 0;\r\n      left: 0;\r\n      right: 0;\r\n      overflow: hidden;\r\n      font-size: 30px;\r\n      text-align: center;\r\n      font-family: sans-serif;\r\n    }\r\n    #target > div {\r\n      padding-top: 3500px;\r\n      animation: autoscroll 1000s linear;\r\n    }\r\n    .stats {\r\n      font-weight: bold;\r\n      color: red;\r\n    }\r\n    .user-stats {\r\n      font-weight: bold;\r\n      margin-top: 100px;\r\n      color: red;\r\n    }\r\n    #target > div > div {\r\n      height: 40px;\r\n    }\r\n    html:after {\r\n      content: '';\r\n      position: absolute;\r\n      top: 0;\r\n      bottom: 0;\r\n      left: 0;\r\n      right: 0;\r\n      background: linear-gradient(top, rgba(0, 0, 0, 1), rgba(0, 0, 0, 0) 100%);\r\n      background: linear-gradient(\r\n        to bottom,\r\n        rgba(0, 0, 0, 1),\r\n        rgba(0, 0, 0, 0) 100%\r\n      );\r\n      pointer-events: none;\r\n    }\r\n\r\n    body {\r\n      position: absolute;\r\n      top: 0;\r\n      bottom: 0;\r\n      left: 0;\r\n      right: 0;\r\n      transform-origin: 50% 100%;\r\n      transform: perspective(600px) rotateX(20deg);\r\n    }\r\n    html {\r\n      color: orange;\r\n    }\r\n    @keyframes autoscroll {\r\n      to {\r\n        margin-top: -50000px;\r\n      }\r\n    }" +
            "</style>";
    }
}
