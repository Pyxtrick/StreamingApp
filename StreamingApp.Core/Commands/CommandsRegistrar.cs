using Microsoft.Extensions.DependencyInjection;
using StreamingApp.API.Utility.Caching.Interface;
using StreamingApp.API.Utility.Caching;
using StreamingApp.Core.Commands.DB;
using StreamingApp.Core.Commands.FileLogic;
using StreamingApp.Core.Commands.Twitch.Interfaces;
using StreamingApp.Core.Commands.Twitch;
using StreamingApp.Core.Queries;
using StreamingApp.Core.Utility.Caching.CacheData;
using StreamingApp.Core.Utility;
using StreamingApp.Core.Commands.Chat;
using StreamingApp.Core.Commands.DB.Interfaces;

namespace StreamingApp.Core.Commands;

public static class QueriesRegistrar
{
    public static void AddCoreCommandOptions(this IServiceCollection services)
    {
        // Chat
        services.AddScoped<ISendSignalRMessage, SendSignalRMessage>();
        services.AddScoped<ITranslate, Translate>();

        // DB
        services.AddScoped<IAddUserToDB, AddUserToDB>();
        services.AddScoped<IUpdateUserOnDB, UpdateUserOnDB>();

        // FileLogic
        services.AddScoped<IManageFile, ManageFile>();
        //services.AddScoped<ITTS, TTS>();
        //services.AddScoped<IAchievements, Achievements>();

        services.AddScoped<IGetTwitchChatData, GetTwitchChatData>();

        //Utility
        services.AddAutoMapper(typeof(CoreMappingProfile));

        services.AddScoped<IQueueCache, QueueCache>();
        services.AddSingleton<QueueData>();

        //Commands
        services.AddScoped<IStartTwitchApi, StartInitialise>();

        services.AddScoped<IAddDBData, AddDBData>();

        // Twitch
        services.AddScoped<IManageMessages, ManageMessages>();
        services.AddScoped<IManageCommands, ManageCommands>();

        services.AddScoped<IManageStream, ManageStream>();

        services.AddScoped<IQueueCommand, QueueCommand>();
        services.AddScoped<IGameCommand, GameCommand>();

        // Web

    }
}
