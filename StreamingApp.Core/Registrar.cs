using Microsoft.Extensions.DependencyInjection;
using StreamingApp.Core.Commands;
using StreamingApp.Core.Queries;

namespace StreamingApp.Core;

public static class Registrar
{
    public static void AddCoreOptions(this IServiceCollection services)
    {
        services.AddCoreCommandOptions();

        services.AddCoreQueryOptions();
    }
}
