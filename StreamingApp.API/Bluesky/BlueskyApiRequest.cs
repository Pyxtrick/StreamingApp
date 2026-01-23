using idunno.AtProto;
using StreamingApp.API.Utility.Caching.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace StreamingApp.API.Bluesky;

public class BlueskyApiRequest
{
    private readonly IBlueskyCache _blueskyCache;

    public BlueskyApiRequest(IBlueskyCache blueskyCache)
    {
        _blueskyCache = blueskyCache;
    }

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
}
