﻿using Microsoft.Extensions.DependencyInjection;
using StreamingApp.API.BetterTV_7TV;
using StreamingApp.API.Interfaces;
using StreamingApp.API.StreamerBot;
using StreamingApp.API.Twitch;
using StreamingApp.API.Twitch.Interfaces;
using StreamingApp.API.Utility;
using StreamingApp.API.Utility.Caching;
using StreamingApp.API.Utility.Caching.CacheData;
using StreamingApp.API.Utility.Caching.Interface;

namespace StreamingApp.API;

public static class APIRegistrar
{
    public static void AddApiOptions(this IServiceCollection services)
    {
        //Discord

        //Emotes
        services.AddScoped<IEmotesApiRequest, EmotesApiRequest>();

        //StreamerBot
        services.AddScoped<IStreamerBotRequest, StreamerBotRequest>();

        //TTS

        //twitch
        services.AddScoped<ITwitchApiRequest, TwitchApiRequest>();
        services.AddScoped<ITwitchPubSubApiRequest, TwitchPubSubApiRequest>();
        services.AddScoped<ITwitchInitialise, TwitchInitialise>();
        services.AddScoped<ITwitchSendRequest, TwitchSendRequest>();

        //YouTube
        services.AddScoped<IYouTubeSendRequest, YoutubeSendRequest>();

        //utility
        services.AddAutoMapper(typeof(TwitchMappingProfile));

        services.AddScoped<ITwitchCache, TwitchCache>();
        services.AddSingleton<TwitchCacheData>();

        services.AddScoped<ITwitchCallCache, TwitchCallCache>();
        services.AddSingleton<TwitchCallCacheData>();

        services.AddScoped<IEmotesCache, EmotesCache>();
        services.AddSingleton<EmotesCacheData>();
    }
}
