using StreamingApp.Domain.Entities.Dtos.Twitch;
using System.Collections.ObjectModel;

namespace StreamingApp.API.Utility.Caching.CacheData;
public class TwitchCallCacheData
{
    public List<MessageDto> CachedMessageData { get; set; } = new List<MessageDto>();

    public List<CommandDto> CachedCommandData { get; set; } = new List<CommandDto>();

    public List<SubDto> CachedGiftedSubData { get; set; } = new List<SubDto>();

    public List<SubDto> CachedNewSubData { get; set; } = new List<SubDto>();

    public List<SubDto> CachedPrimeSubData { get; set; } = new List<SubDto>();

    public List<SubDto> CachedReSubData { get; set; } = new List<SubDto>();

    public List<RaidDto> CachedRaidData { get; set; } = new List<RaidDto>();

    public List<JoinDto> CachedUserJoinData { get; set; } = new List<JoinDto>();
    //public ObservableCollection<JoinDto> CachedUserJoinData { get; set; }

    // problebly do not need it
    //public IList<> CachedBannedData { get; set; } = new List<>();
}
