using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace StreamingApp.DB;

public static class Registrar
{
    public static void AddDataBaseFeature(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<UnitOfWorkContext>(s => s.UseSqlServer(connectionString));
    }
}
