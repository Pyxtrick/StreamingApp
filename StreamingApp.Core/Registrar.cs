using Microsoft.Extensions.DependencyInjection;
using StreamingApp.API.Utility.Caching;
using StreamingApp.API.Utility.Caching.Interface;
using StreamingApp.Core.Commands;
using StreamingApp.Core.Commands.DB;
using StreamingApp.Core.Commands.FileLogic;
using StreamingApp.Core.Commands.Twitch;
using StreamingApp.Core.Commands.Twitch.Interfaces;
using StreamingApp.Core.Queries;
using StreamingApp.Core.Queries.Web;
using StreamingApp.Core.Queries.Web.Interfaces;
using StreamingApp.Core.Utility;
using StreamingApp.Core.Utility.Caching.CacheData;
using StreamingApp.Core.Utility.Scheduler;

namespace StreamingApp.Core;

public static class Registrar
{
    public static void AddCoreOptions(this IServiceCollection services)
    {
        services.AddCoreCommandOptions();



        services.AddCoreQueryOptions();
    }
}
