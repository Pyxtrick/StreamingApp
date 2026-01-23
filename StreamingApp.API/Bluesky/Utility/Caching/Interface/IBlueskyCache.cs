using idunno.Bluesky;

namespace StreamingApp.API.Utility.Caching.Interface;

public interface IBlueskyCache
{
    void AddData(BlueskyAgent blueskyAgent);
    BlueskyAgent GetBlueskyAgent();
}