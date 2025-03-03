using Microsoft.Extensions.DependencyInjection;
using StreamingApp.Core.Commands;
using StreamingApp.Core.Commands.Achievements;
using StreamingApp.Core.Commands.FileLogic;
using StreamingApp.Core.Logic;
using StreamingApp.Core.Logic.Interfaces;
using StreamingApp.Core.Queries;
using StreamingApp.Core.Utility.Scheduler;

namespace StreamingApp.Core;

public static class Registrar
{
    public static void AddCoreOptions(this IServiceCollection services)
    {
        services.AddCoreCommandOptions();

        services.AddCoreQueryOptions();

        // Schedulars
        services.AddHostedService<ActivityScheduler>();
        //services.AddHostedService<AlertScheduler>();
        //services.AddHostedService<BannedScheduler>();

        //Logic 
        services.AddScoped<IMessageCheck, MessageCheck>();

        // Achievements
        services.AddScoped<ICreateFinalStreamAchievements, CreateFinalStreamAchievements>();

        // File
        services.AddScoped<IFileAchievements , FileAchievements>();
        services.AddScoped<IManageFile, ManageFile>();
    }
}
