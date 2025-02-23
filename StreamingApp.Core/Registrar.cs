using Microsoft.Extensions.DependencyInjection;
using StreamingApp.Core.Commands;
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
    }
}
