using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Persistence.Extensions;

public static class ServiceCollectionExtension
{
    public static void AddPersistenceServices(this IServiceCollection services, IConfiguration configuration, string connectionStringSectionName)
    {
        var connection = configuration.GetConnectionString(connectionStringSectionName);

        services.AddDbContext<DbContext, InnoStoreContext>((serviceProvider, options) =>
        {
            options.UseNpgsql(connection, b => b.MigrationsAssembly(typeof(InnoStoreContext).Assembly.GetName().Name));
        });
    }
}
