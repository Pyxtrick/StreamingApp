﻿using Microsoft.Extensions.DependencyInjection;
using StreamingApp.API.Twitch;
using StreamingApp.API.Twitch.Interfaces;
using StreamingApp.API.Utility.Caching;
using StreamingApp.API.Utility.Caching.CacheData;
using StreamingApp.API.Utility.Caching.Interface;

namespace StreamingApp.API;

public static class Registrar
{
    public static void AddApiOptions(this IServiceCollection services)
    {
        //utility
        services.AddScoped<ITwitchCache, TwitchCache>();
        services.AddSingleton<TwitchCacheData>();

        services.AddScoped<ITwitchCallCache, TwitchCallCache>();
        services.AddSingleton<TwitchCallCacheData>();

        //twitch
        services.AddScoped<ITwitchApiRequest, TwitchApiRequest>();
        services.AddScoped<ITwitchInitialise, TwitchInitialise>();
    }
}
