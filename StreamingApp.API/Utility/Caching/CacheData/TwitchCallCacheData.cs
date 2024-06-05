using StreamingApp.Domain.Entities.Dtos.Twitch;

namespace StreamingApp.API.Utility.Caching.CacheData;
public class TwitchCallCacheData
{
    public IList<MessageDto> CachedMessageData { get; set; } = new List<MessageDto>();

    public IList<CommandDto> CachedCommandData { get; set; } = new List<CommandDto>();

    public IList<SubDto> CachedGiftedSubData { get; set; } = new List<SubDto>();

    public IList<SubDto> CachedNewSubData { get; set; } = new List<SubDto>();

    public IList<SubDto> CachedPrimeSubData { get; set; } = new List<SubDto>();

    public IList<SubDto> CachedReSubData { get; set; } = new List<SubDto>();

    public IList<RaidDto> CachedRaidData { get; set; } = new List<RaidDto>();

    public IList<JoinDto> CachedUserJoinData { get; set; } = new List<JoinDto>();

    // problebly do not need it
    //public IList<> CachedBannedData { get; set; } = new List<>();
}
