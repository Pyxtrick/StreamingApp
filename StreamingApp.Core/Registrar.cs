using Microsoft.Extensions.DependencyInjection;
using StreamingApp.API.Utility.Caching;
using StreamingApp.API.Utility.Caching.Interface;
using StreamingApp.Core.Commands;
using StreamingApp.Core.Commands.Bluesky;
using StreamingApp.Core.Commands.DB.CRUD;
using StreamingApp.Core.Commands.DB.CRUD.Interfaces;
using StreamingApp.Core.Commands.FileLogic;
using StreamingApp.Core.Commands.Hub;
using StreamingApp.Core.Commands.Twitch;
using StreamingApp.Core.Commands.Twitch.Interfaces;
using StreamingApp.Core.Queries.Achievements;
using StreamingApp.Core.Queries.Alerts;
using StreamingApp.Core.Queries.Alerts.Interfaces;
using StreamingApp.Core.Queries.Logic;
using StreamingApp.Core.Queries.Logic.Interfaces;
using StreamingApp.Core.Queries.Translate;
using StreamingApp.Core.Utility;
using StreamingApp.Core.Utility.Caching.CacheData;
using StreamingApp.Core.Utility.Scheduler;
using StreamingApp.Core.Utility.TextToSpeach;
using StreamingApp.Core.Utility.TextToSpeach.Cache;
using StreamingApp.Core.Utility.TextToSpeach.Cache.CacheData;
using StreamingApp.Core.VTubeStudio.Cache.CacheData;
using StreamingApp.Core.VTubeStudio.Cache.Interface;

namespace StreamingApp.Core;

public static class Registrar
{
    public static void AddCoreOptions(this IServiceCollection services, string automapperKey)
    {
        #region Commands
        // Bluesky
        services.AddScoped<IManageTweets, ManageTweets>();

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
        services.AddScoped<IManageAchievements, ManageAchievements>();
        services.AddScoped<IManageAlert, ManageAlert>();
        services.AddScoped<IManageCommands, ManageCommands>();
        services.AddScoped<IManageCommandsWithLogic, ManageCommandsWithLogic>();
        services.AddScoped<IManageDeleted, ManageDeleted>();
        services.AddScoped<IManageMessages, ManageMessages>();
        services.AddScoped<IManageScheduler, ManageScheduler>();
        services.AddScoped<IManageStream, ManageStream>();
        //services.AddScoped<IManageSubathon, ManageSubathon>();
        services.AddScoped<IPointRedeam, PointRedeam>();
        services.AddScoped<IQueueCommand, QueueCommand>();

        //Commands
        services.AddScoped<IStartTwitchApi, StartInitialise>();
        services.AddScoped<IAddDBData, AddDBData>();
        #endregion

        #region Queries
        // Achievements
        services.AddScoped<ICreateFinalStreamAchievements, CreateFinalStreamAchievements>();

        //Alert
        services.AddScoped<IHighlightMessage, HighlightMessage>();
        services.AddScoped<IMovingText, MovingText>();
        services.AddScoped<IRaidAlert, RaidAlert>();
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
        services.AddAutoMapper(cfg => cfg.LicenseKey = automapperKey, typeof(CoreMappingProfile));

        services.AddScoped<IQueueCache, QueueCache>();
        services.AddSingleton<QueueData>();

        // TTS
        services.AddScoped<IManageTextToSpeach, ManageTextToSpeach>();

        services.AddScoped<ITTSCache, TTSCache>();
        services.AddSingleton<TTSCacheData>();

        // File
        services.AddScoped<IFileAchievements , FileAchievements>();
        services.AddScoped<IManageFile, ManageFile>();

        //VtubeStudio
        services.AddScoped<IVtubeStudioCache, VtubeStudioCache>();
        services.AddSingleton<VtubeStudioCacheData>();
    }
}
