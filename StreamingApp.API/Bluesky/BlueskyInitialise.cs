using idunno.Bluesky;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using StreamingApp.API.Twitch;
using StreamingApp.API.Utility.Caching.Interface;

namespace StreamingApp.API.Bluesky;

public class BlueskyInitialise : IBlueskyInitialise
{
    private readonly ILogger<TwitchInitialise> _logger;

    private readonly IConfiguration _configuration;
    private readonly IBlueskyCache _blueskyCache;

    public BlueskyInitialise(ILogger<TwitchInitialise> logger, IConfiguration configuration, IBlueskyCache blueskyCache)
    {
        _logger = logger;
        _configuration = configuration;
        _blueskyCache = blueskyCache;
    }

    public async Task Initialise()
    {
        BlueskyAgent agent = new();

        var loginResult = await agent.Login(_configuration["Bluesky:UserName"], _configuration["Bluesky:Password"]);
        if (loginResult.Succeeded)
        {
            _blueskyCache.AddData(agent);

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Bluesky Login Succesfull");
            Console.ResetColor();
        }
        else
        {
            _logger.LogCritical("Bluesky Login Fail");
        }
    }
}
