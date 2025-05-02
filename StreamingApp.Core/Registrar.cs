using Microsoft.Extensions.DependencyInjection;
using StreamingApp.API.Utility.Caching.Interface;
using StreamingApp.API.Utility.Caching;
using StreamingApp.Core.Commands;
using StreamingApp.Core.Commands.FileLogic;
using StreamingApp.Core.Queries;
using StreamingApp.Core.Utility;
using StreamingApp.Core.Utility.Caching.CacheData;
using StreamingApp.Core.Utility.Scheduler;
using StreamingApp.Core.Queries.Logic;
using StreamingApp.Core.Queries.Logic.Interfaces;
using StreamingApp.Core.Queries.Achievements;
using StreamingApp.Core.Queries.Translate;
using StreamingApp.Core.Commands.Hub;
using StreamingApp.Core.Commands.Twitch.Interfaces;
using StreamingApp.Core.Commands.Twitch;
using StreamingApp.Core.Commands.DB.CRUD;
using StreamingApp.Core.Commands.DB.CRUD.Interfaces;
using StreamingApp.Core.Queries.Alerts;

namespace StreamingApp.Core;

public static class Registrar
{
    public static void AddCoreOptions(this IServiceCollection services)
    {
        #region Commands
        // DB
        services.AddScoped<ICRUDCommands, CRUDCommands>();
        services.AddScoped<ICRUDGameInfos, CRUDGameInfos>();
        services.AddScoped<ICRUDSettings, CRUDSettings>();
        services.AddScoped<ICRUDSpecialWords, CRUDSpecialWords>();
        services.AddScoped<ICRUDStreams, CRUDStreams>();
        services.AddScoped<ICRUDUsers, CRUDUsers>();

        // FileLogic
        services.AddScoped<IManageFile, ManageFile>();
        //services.AddScoped<ITTS, TTS>();
        //services.AddScoped<IAchievements, Achievements>();

        // Hub
        services.AddScoped<ISendSignalRMessage, SendSignalRMessage>();

        // Twitch
        services.AddScoped<IGameCommand, GameCommand>();
        services.AddScoped<IManageCommands, ManageCommands>();
        services.AddScoped<IManageDeleted, ManageDeleted>();
        services.AddScoped<IManageScheduler, ManageScheduler>();
        services.AddScoped<IManageMessages, ManageMessages>();
        services.AddScoped<IManageStream, ManageStream>();
        //services.AddScoped<IManageSub,  ManageSub>();
        //services.AddScoped<IManageSubathon,  ManageSubathon>();
        services.AddScoped<IQueueCommand, QueueCommand>();

        //Commands
        services.AddScoped<IStartTwitchApi, StartInitialise>();
        services.AddScoped<IAddDBData, AddDBData>();
        #endregion

        #region Queries
        // Achievements
        services.AddScoped<ICreateFinalStreamAchievements, CreateFinalStreamAchievements>();

        //Alert
        services.AddScoped<ISubAlertLoong, SubAlertLoong>();

        //Logic 
        services.AddScoped<IMessageCheck, MessageCheck>();
        //services.AddScoped<Customcode, Customcode>();
        //services.AddScoped<Morsecode, Morsecode>();

        // Chat
        services.AddScoped<ITranslate, Translate>();
        #endregion

        // Schedulars
        services.AddHostedService<AlertMessageScheduler>();
        services.AddHostedService<MessageScheduler>();
        services.AddHostedService<BannedScheduler>();
        services.AddHostedService<AlertScheduler>();
        //services.AddHostedService<YoutubeScheduler>();

        //Utility
        services.AddAutoMapper(typeof(CoreMappingProfile));

        services.AddScoped<IQueueCache, QueueCache>();
        services.AddSingleton<QueueData>();

        // File
        services.AddScoped<IFileAchievements , FileAchievements>();
        services.AddScoped<IManageFile, ManageFile>();
    }
}
