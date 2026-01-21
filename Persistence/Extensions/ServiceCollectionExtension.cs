using Domain.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using Persistence.DataInitializers;
using Persistence.DataInitializers.Abstractions;
using Persistence.Interceptors;
using Persistence.Repositories;

namespace Persistence.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        var builder = new NpgsqlConnectionStringBuilder
        {
            Host = configuration["DB_HOST"],
            Port = int.Parse(configuration["DB_PORT"] ?? "5432"),
            Database = configuration["DB_NAME"],
            Username = configuration["DB_USER"],
            Password = configuration["DB_PASSWORD"]
        };

        services.AddDbContext<InnoStoreContext>(options =>
            options.UseNpgsql(builder.ConnectionString));

        return services;
    }

    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IProductGroupRepository, ProductGroupRepository>();
        services.AddScoped<IPassedEventRepository, PassedEventRepository>();
    }

    public static void AddInterceptors(this IServiceCollection services)
    {
        services.AddScoped<IInterceptor, SetEntityDetailsInterceptor>();
    }

    public static void AddInitiaizers(this IServiceCollection services)
    {
        services.AddScoped<IDataInitializer, ProductGroupInitializer>();
    }
}
