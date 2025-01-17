using Microsoft.Extensions.DependencyInjection;
using StreamingApp.Core.Queries.Web.Interfaces;
using StreamingApp.Core.Queries.Web;
using StreamingApp.Core.Utility.Scheduler;

namespace StreamingApp.Core.Queries;

public static class QueriesRegistrar
{
    public static void AddCoreQueryOptions(this IServiceCollection services)
    {
        //services.AddScoped<IGetTwitchDataQuery, GetTwitchDataQuery>();

        // Schedulars
        services.AddHostedService<ActivityScheduler>();

        // Web
        services.AddScoped<IDeleteCommands, DeleteCommands>();
        services.AddScoped<IDeleteSpecialWords, DeleteSpecialWords>();
        services.AddScoped<IGetCommands, GetCommands>();
        services.AddScoped<IGetSpecialWords, GetSpecialWords>();
        services.AddScoped<IUpdateCommands, UpdateCommands>();
        services.AddScoped<IUpdateSpecialWords, UpdateSpecialWords>();

    }
}
