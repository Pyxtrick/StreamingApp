using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StreamingApp.API.BetterTV_7TV;
using StreamingApp.API.Bluesky;
using StreamingApp.API.Bluesky.Interfaces;
using StreamingApp.API.Interfaces;
using StreamingApp.API.StreamerBot;
using StreamingApp.API.TTS;
using StreamingApp.API.Twitch;
using StreamingApp.API.Twitch.Interfaces;
using StreamingApp.API.Utility;
using StreamingApp.API.Utility.Caching;
using StreamingApp.API.Utility.Caching.CacheData;
using StreamingApp.API.Utility.Caching.Interface;
using StreamingApp.API.Youtube;
using YTLiveChat.Contracts;
using YTLiveChat.DependencyInjection;

namespace StreamingApp.API;

public static class APIRegistrar
{
    public static void AddApiOptions(this IServiceCollection services, string automapperKey, IConfiguration configuration)
    {
        //Bluesky
        services.AddScoped<IBlueskyApiRequest, BlueskyApiRequest>();
        services.AddScoped<IBlueskyInitialise, BlueskyInitialise>();

        services.AddScoped<IBlueskyCache, BlueskyCache>();
        services.AddSingleton<BlueskyCacheData>();

        //Discord

        //Emotes
        services.AddScoped<IEmotesApiRequest, EmotesApiRequest>();

        //StreamerBot
        services.AddScoped<IStreamerBotRequest, StreamerBotRequest>();

        //TTS
        services.AddScoped<ITTSApiRequest, TTSApiRequest>();

        //twitch
        services.AddScoped<ITwitchApiRequest, TwitchApiRequest>();
        services.AddScoped<ITwitchPubSubApiRequest, TwitchPubSubApiRequest>();
        services.AddScoped<ITwitchInitialise, TwitchInitialise>();
        services.AddScoped<ITwitchSendRequest, TwitchSendRequest>();

        services.AddScoped<ITwitchCache, TwitchCache>();
        services.AddSingleton<TwitchCacheData>();

        services.AddScoped<ITwitchCallCache, TwitchCallCache>();
        services.AddSingleton<TwitchCallCacheData>();

        //YouTube
        services.AddYTLiveChat(configuration);
        services.Configure<YTLiveChatOptions>(options =>
        {
            options.RequestFrequency = 1000;
            options.DebugLogReceivedJsonItems = true;
            options.DebugLogFilePath = "logs/ytlivechat_debug.json";
        });
        services.AddHostedService<ChatWorker>();

        services.AddScoped<IYouTubeSendRequest, YoutubeSendRequest>();
        services.AddScoped<IYoutubeInitialise, YoutubeInitialise>();

        //utility
        services.AddAutoMapper(cfg => cfg.LicenseKey = automapperKey, typeof(TwitchMappingProfile));
        /**services.AddSingleton(provider => new MapperConfiguration(cfg =>
        {
            cfg.LicenseKey = automapperKey;
            cfg.AddProfile(new TwitchMappingProfile(provider.GetService<IConfiguration>()));
        }, NullLoggerFactory.Instance).CreateMapper());
        **/
        services.AddScoped<IEmotesCache, EmotesCache>();
        services.AddSingleton<EmotesCacheData>();
    }
}
