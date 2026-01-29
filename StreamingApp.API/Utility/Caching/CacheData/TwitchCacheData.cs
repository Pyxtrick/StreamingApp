using TwitchLib.Api;
using TwitchLib.Client;

namespace StreamingApp.API.Utility.Caching.CacheData;

public class TwitchCacheData
{
    public TwitchClient OwnerOfChannelConnection { get; set; } = new();

    public TwitchAPI TheTwitchAPI { get; set; } = new();

    public TwitchAPI AppTwitchAPI { get; set; } = new();

    public IList<Object> UnusedData { get; set; } = new List<Object>();
}
