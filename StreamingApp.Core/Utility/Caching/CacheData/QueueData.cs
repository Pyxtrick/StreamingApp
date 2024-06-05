using StreamingApp.Domain.Entities.Dtos;

namespace StreamingApp.Core.Utility.Caching.CacheData;

public class QueueData
{
    public IList<UserQueueDto> CachedQueueData { get; set; } = new List<UserQueueDto>();
}
