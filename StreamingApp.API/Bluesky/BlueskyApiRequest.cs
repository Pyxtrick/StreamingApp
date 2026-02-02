using idunno.Bluesky;
using idunno.Bluesky.RichText;
using StreamingApp.API.Bluesky.Interfaces;
using StreamingApp.API.Utility.Caching.Interface;

namespace StreamingApp.API.Bluesky;

public class BlueskyApiRequest : IBlueskyApiRequest
{
    private readonly IBlueskyCache _blueskyCache;

    public BlueskyApiRequest(IBlueskyCache blueskyCache)
    {
        _blueskyCache = blueskyCache;
    }

    /// <summary>
    /// 
    /// https://bluesky.idunno.dev/docs/tutorials/creatingAPost.html
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    public async Task<bool> PostTweet(string message)
    {
        var response = await _blueskyCache.GetBlueskyAgent().Post(message);
        if (response.Succeeded)
        {
            Console.WriteLine(response.Result);
            return true;
        }

        return false;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="postBuilder"></param>
    /// <returns></returns>
    public async Task<bool> PostTweet(PostBuilder postBuilder)
    {
        var response = await _blueskyCache.GetBlueskyAgent().Post(postBuilder);
        if (response.Succeeded)
        {
            Console.WriteLine(response.Result);
            return true;
        }

        return false;
    }
}
