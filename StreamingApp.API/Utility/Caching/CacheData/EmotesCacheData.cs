using StreamingApp.Domain.Entities.Dtos;

namespace StreamingApp.API.Utility.Caching.CacheData;

public class EmotesCacheData
{
    public List<EmoteDto> EmoteData { get; set; } = new List<EmoteDto>();
}
