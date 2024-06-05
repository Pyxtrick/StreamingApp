using StreamingApp.Domain.Entities.Dtos;

namespace StreamingApp.API.Utility.Caching.CacheData;

public class EmotesCacheData
{
    public IList<EmoteDto> _7TVEmoteData { get; set; } = new List<EmoteDto>();
    public IList<EmoteDto> BttvEmoteData { get; set; } = new List<EmoteDto>();
    public IList<EmoteDto> TwitchEmoteData { get; set; } = new List<EmoteDto>();
}
