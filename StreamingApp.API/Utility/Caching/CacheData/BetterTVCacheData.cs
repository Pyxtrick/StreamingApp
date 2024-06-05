using StreamingApp.Domain.Entities.Dtos;

namespace StreamingApp.API.Utility.Caching.CacheData;

public class BetterTVCacheData
{
    public IList<EmoteDto> BetterTVEmoteData { get; set; } = new List<EmoteDto>();
}
