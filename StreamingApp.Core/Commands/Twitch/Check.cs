using Microsoft.Extensions.Configuration;
using StreamingApp.API.Utility.Caching.Interface;
using StreamingApp.Domain.Entities.Dtos.Twitch;
using StreamingApp.Domain.Enums;

namespace StreamingApp.Core.Commands.Twitch;

public class Check : ICheck
{
    private readonly ITwitchCallCache _twitchCallCache;

    private readonly IConfiguration _configuration;

    public Check(ITwitchCallCache twitchCallCache, IConfiguration configuration)
    {
        _twitchCallCache = twitchCallCache;
        _configuration = configuration;
    }

    public bool CheckAuth(AuthEnum messageAuth, List<AuthEnum> userAuth)
    {
        foreach (var auth in userAuth)
        {
            int id = (int)auth;
            if ((int)auth <= (int)messageAuth)
            {
                return true;
            }
        }

        return false;
    }

    public bool CheckIfCommandAvalibleToUse(string message, List<AuthEnum> auth)
    {
        if (auth.Contains(AuthEnum.Mod))
        {
            return true;
        }

        // Check the last times is longer than x minutes appart
        // TODO: Change 1 to be from Appsettings or DB
        var timer = Int32.Parse(_configuration["Scheduler:Activity"]);

        IList<MessageDto> chatDtos = (IList<MessageDto>)((IList<MessageDto>)_twitchCallCache.GetAllMessagesFromTo(DateTime.UtcNow.AddMinutes(timer * -1), DateTime.UtcNow, CallCacheEnum.CachedMessageData)).Where(m => m.IsCommand);

        chatDtos.OrderBy(c => c.Date);

        if (chatDtos.Count >= 2)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}