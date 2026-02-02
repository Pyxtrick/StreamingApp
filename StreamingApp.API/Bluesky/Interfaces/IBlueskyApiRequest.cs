using idunno.Bluesky;

namespace StreamingApp.API.Bluesky.Interfaces;

public interface IBlueskyApiRequest
{
    Task<bool> PostTweet(string message);
    Task<bool> PostTweet(PostBuilder postBuilder);
}