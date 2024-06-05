using StreamingApp.API.Utility.Caching.Interface;
using StreamingApp.Domain.Entities.Dtos.Twitch;
using StreamingApp.Domain.Enums;

namespace StreamingApp.Core.Commands.Twitch;

public class Check : ICheck
{
    ITwitchCallCache _twitchCallCache;

    public Check(ITwitchCallCache twitchCallCache)
    {
        _twitchCallCache = twitchCallCache;
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
        IList<CommandDto> chatDtos = (IList<CommandDto>)_twitchCallCache.GetAllMessagesFromTo(DateTime.UtcNow.AddMinutes(-1), DateTime.UtcNow, CallCacheEnum.CachedCommandData);
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