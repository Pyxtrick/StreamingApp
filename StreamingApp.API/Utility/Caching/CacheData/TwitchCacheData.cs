using System.ComponentModel.DataAnnotations;
using TwitchLib.Api;
using TwitchLib.Client;

namespace StreamingApp.API.Utility.Caching.CacheData;

public class TwitchCacheData
{
    public TwitchClient OwnerOfChannelConnection { get; set; } = new();

    public TwitchAPI TheTwitchAPI { get; set; } = new();

    [Required]
    public string TwitchChannelName { get; set; }

    [Required]
    public string TwitchChannelID { get; set; }

    public IList<Object> UnusedData { get; set; } = new List<Object>();
}
