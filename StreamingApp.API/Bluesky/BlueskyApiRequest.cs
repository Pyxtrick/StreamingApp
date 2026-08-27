using idunno.Bluesky;
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

    public async Task<bool> SetLiveStatus(string title, string description, int durationMinutes, bool IsStart)
    {
        var urltext = "https://twitch.tv/pyxtrick";

        if (IsStart)
        {
            var createLiveStatusResult = await _blueskyCache.GetBlueskyAgent().CreateLiveStatus(
                uri: new Uri(urltext),
                title: title,
                description: description,
                durationMinutes: durationMinutes);

            if (createLiveStatusResult.Succeeded)
            {
                return true;
            }
        }
        else
        {
            var deleteStatusResult = await _blueskyCache.GetBlueskyAgent().DeleteStatus();

            Console.WriteLine(deleteStatusResult.Succeeded);

            return true;
        }

        return false;
    }
}
