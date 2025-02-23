using StreamingApp.Domain.Entities.Dtos;
using StreamingApp.Domain.Entities.Dtos.Twitch;

namespace StreamingApp.API.Utility.Caching.CacheData;
public class TwitchCallCacheData
{
    public List<MessageDto> CachedMessageData { get; set; } = new();

    public List<SubDto> CachedSubData { get; set; } = new();

    public List<RaidDto> CachedRaidData { get; set; } = new();

    public List<FollowDto> CachedUserFollowData { get; set; } = new();

    public List<BannedUserDto> CachedBannedData { get; set; } = new();
    //public ObservableCollection<JoinDto> CachedUserJoinData { get; set; }

    // problebly do not need it
    //public IList<> CachedBannedData { get; set; } = new List<>();
}
