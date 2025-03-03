using Microsoft.EntityFrameworkCore;
using StreamingApp.API.Utility.Caching.Interface;
using StreamingApp.Core.Commands.FileLogic;
using StreamingApp.DB;
using StreamingApp.Domain.Enums;

namespace StreamingApp.Core.Commands.Achievements;

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

        var usersFound = _unitOfWork.User.Include("TwitchAchievements").Where(u => u.TwitchAchievements.LastStreamSeen >= stream.StreamStart && u.TwitchAchievements.LastStreamSeen <= DateTime.UtcNow);

        var messageCount = _twitchCallCache.GetChachedNumberCount(CallCacheEnum.CachedMessageData);
        var subCount = _twitchCallCache.GetChachedNumberCount(CallCacheEnum.CachedSubData);
        var raidCount = _twitchCallCache.GetChachedNumberCount(CallCacheEnum.CachedRaidData);
        var raidUserCount = _twitchCallCache.GetChachedNumberCount(CallCacheEnum.CachedRaidUserData);
        var bannedCount = _twitchCallCache.GetChachedNumberCount(CallCacheEnum.CachedBannedData);

        List<string> twitchAchievements = new List<string>();

        var fileText = _manageFile.ReadFile();

        foreach (var item in fileText)
        {
            string[] parts = item.Split('-');

            if (parts.Length == 4)
            {
                if (parts[0].Contains(nameof(ChatOriginEnum.Twtich)))
                {
                    twitchAchievements.Add($"User: {parts[1]} has gifted {parts[3]} {parts[2]}");
                }
            }
        }

        return CreateHdmlFile(messageCount, usersFound.Count(), subCount, raidCount, raidUserCount, bannedCount, twitchAchievements);
    }

    private string CreateHdmlFile(int messageCount, int userChatted, int subCount, int raidCount, int raidUserCount, int bannedCount, List<string> twitchAchievements)
    {
        var messageText = messageCount != 0 ? $"<div>Messages Recived: {messageCount} Sent by {userChatted} Users</div>" : "";
        var subText = subCount != 0 ? $"<div>Subs Recived: {subCount}</div>" : "";
        var raidText = raidCount != 0 ? $"<div>{raidCount} Raids with {raidUserCount} Users</div>" : "";
        var userText = twitchAchievements.Count() != 0 ? $"<div>{String.Join("</a><a>", twitchAchievements)}</div>" : "";

        return "<html lang=\"en\">\r\n  <div id=\"breaking-news-container\">" +
            "<div>" +
                "<div>" +
                    "<div class=\"stats\">Stream Stats</div>" +
                    $"{messageText}" +
                    $"{subText}" +
                    $"{raidText}" +
                    "<div class=\"user-stats\">Stream Stats</div>" +
                    $"{userText}" +
                "</div>" +
            "</div>" +
            "<style>" +
                "#target {\r\n      position: absolute;\r\n      top: -2300px;\r\n      bottom: 0;\r\n      left: 0;\r\n      right: 0;\r\n      overflow: hidden;\r\n      font-size: 30px;\r\n      text-align: center;\r\n      font-family: sans-serif;\r\n    }\r\n    #target > div {\r\n      padding-top: 3500px;\r\n      animation: autoscroll 1000s linear;\r\n    }\r\n    .stats {\r\n      font-weight: bold;\r\n      color: red;\r\n    }\r\n    .user-stats {\r\n      font-weight: bold;\r\n      margin-top: 100px;\r\n      color: red;\r\n    }\r\n    #target > div > div {\r\n      height: 40px;\r\n    }\r\n    html:after {\r\n      content: '';\r\n      position: absolute;\r\n      top: 0;\r\n      bottom: 0;\r\n      left: 0;\r\n      right: 0;\r\n      background: linear-gradient(top, rgba(0, 0, 0, 1), rgba(0, 0, 0, 0) 100%);\r\n      background: linear-gradient(\r\n        to bottom,\r\n        rgba(0, 0, 0, 1),\r\n        rgba(0, 0, 0, 0) 100%\r\n      );\r\n      pointer-events: none;\r\n    }\r\n\r\n    body {\r\n      position: absolute;\r\n      top: 0;\r\n      bottom: 0;\r\n      left: 0;\r\n      right: 0;\r\n      transform-origin: 50% 100%;\r\n      transform: perspective(600px) rotateX(20deg);\r\n    }\r\n    html {\r\n      color: yellow;\r\n    }\r\n    @keyframes autoscroll {\r\n      to {\r\n        margin-top: -50000px;\r\n      }\r\n    }" +
            "</style>";
    }
}
