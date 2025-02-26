using StreamingApp.Domain.Entities.Dtos;

namespace StreamingApp.API.Utility.Caching.CacheData;

public class EmotesCacheData
{
    public IList<EmoteDto> EmoteData { get; set; } = new List<EmoteDto>();
}
