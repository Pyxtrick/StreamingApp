using StreamingApp.Domain.Entities.Dtos;
using StreamingApp.Domain.Entities.Dtos.Twitch;

namespace StreamingApp.API.Utility.Caching.CacheData;
public class TwitchCallCacheData
{
    public List<MessageDto> CachedMessageData { get; set; } = new();

    public int CachedMessageNumber = 0;

    public List<SubDto> CachedSubData { get; set; } = new();

    public int CachedSubNumber = 0;

    public List<RaidDto> CachedRaidData { get; set; } = new();

    public int CachedRaidNumber = 0;
    public int CachedRaidUserNumber = 0;

    public List<FollowDto> CachedUserFollowData { get; set; } = new();

    public int CachedUserFollowNumber = 0;

    public List<BannedUserDto> CachedBannedData { get; set; } = new();

    public int CachedBannedNumber = 0;
    //public ObservableCollection<JoinDto> CachedUserJoinData { get; set; }

    // problebly do not need it
    //public IList<> CachedBannedData { get; set; } = new List<>();
}
