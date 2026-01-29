using StreamingApp.API.Utility.Caching.CacheData;
using StreamingApp.API.Utility.Caching.Interface;
using TwitchLib.Api;
using TwitchLib.Client;

namespace StreamingApp.API.Utility.Caching;

public class TwitchCache : ITwitchCache
{
    private readonly TwitchCacheData _twitchCacheData;

    public TwitchCache(TwitchCacheData twitchCacheData)
    {
        _twitchCacheData = twitchCacheData;
    }
    public TwitchClient GetOwnerOfChannelConnection()
    {
        return _twitchCacheData.OwnerOfChannelConnection;
    }

    public TwitchAPI GetTheTwitchAPI()
    {
        return _twitchCacheData.TheTwitchAPI;
    }

    public TwitchAPI GetAppTwitchAPI()
    {
        return _twitchCacheData.AppTwitchAPI;
    }

    public void AddData(TwitchClient twitchClient, TwitchAPI TheTwitchAPI, TwitchAPI AppTwitchAPI)
    {
        if (twitchClient != null)
        {
            _twitchCacheData.OwnerOfChannelConnection = twitchClient;
        }
        _twitchCacheData.TheTwitchAPI = TheTwitchAPI;

        _twitchCacheData.AppTwitchAPI = AppTwitchAPI;
    }

    public void AddUnusedData(object unused)
    {
        _twitchCacheData.UnusedData.Add(unused);
    }

    public IList<Object> GetUnusedData()
    {
        return _twitchCacheData.UnusedData;
    }
}
