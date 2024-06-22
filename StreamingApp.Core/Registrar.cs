using Microsoft.Extensions.DependencyInjection;
using StreamingApp.API.Utility.Caching;
using StreamingApp.API.Utility.Caching.Interface;
using StreamingApp.Core.Commands;
using StreamingApp.Core.Commands.DB;
using StreamingApp.Core.Commands.Interfaces;
using StreamingApp.Core.Commands.Twitch;
using StreamingApp.Core.Commands.Twitch.Interfaces;
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
        //Utility
        services.AddAutoMapper(typeof(CoreMappingProfile));

        services.AddScoped<IQueueCache, QueueCache>();
        services.AddSingleton<QueueData>();

        //Commands
        services.AddScoped<IStartTwitchApi, StartInitialise>();

        services.AddScoped<IAddDBData, AddDBData>();

        services.AddScoped<ICheck, Check>();

        // Twitch
        services.AddScoped<IManageMessages, ManageMessages>();
        services.AddScoped<IManageCommands, ManageCommands>();

        services.AddScoped<IQueueCommand, QueueCommand>();
        services.AddScoped<IGameCommand, GameCommand>();

        // DB
        services.AddScoped<IAddUserToDB, AddUserToDB>();
        services.AddScoped<IUpdateUserAchievementsOnDB, UpdateUserAchievementsOnDB>();

        // Web


        //Queries
        //services.AddScoped<IGetTwitchDataQuery, GetTwitchDataQuery>();

        // Schedulars
        services.AddHostedService<ActivityScheduler>();

        // Web
        services.AddScoped<IGetCommands, GetCommands>();
    }
}
