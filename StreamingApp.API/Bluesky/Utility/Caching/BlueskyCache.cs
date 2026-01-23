using idunno.Bluesky;
using StreamingApp.API.Utility.Caching.CacheData;
using StreamingApp.API.Utility.Caching.Interface;

namespace StreamingApp.API.Utility.Caching;

public class BlueskyCache : IBlueskyCache
{
    private readonly BlueskyCacheData _blueskyCacheData;

    public BlueskyCache(BlueskyCacheData blueskyCacheData)
    {
        _blueskyCacheData = blueskyCacheData;
    }

    public BlueskyAgent GetBlueskyAgent()
    {
        return _blueskyCacheData.BlueskyAgent;
    }

    public void AddData(BlueskyAgent blueskyAgent)
    {
        if (blueskyAgent != null)
        {
            _blueskyCacheData.BlueskyAgent = blueskyAgent;
        }
    }
}
