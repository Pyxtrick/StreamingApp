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

        // Web
        services.AddScoped<ICRUDCommands, CRUDCommands>();
        services.AddScoped<ICRUDGameInfos, CRUDGameInfos>();
        services.AddScoped<ICRUDSpecialWords, CRUDSpecialWords>();
        services.AddScoped<ICRUDStreams, CRUDStreams>();
    }
}
