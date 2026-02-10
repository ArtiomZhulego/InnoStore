using Domain.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using Persistence.DataInitializers;
using Persistence.DataInitializers.Abstractions;
using Persistence.ExternalServices;
using Persistence.DatabaseManagers;
using Persistence.Interceptors;
using Persistence.Repositories;

namespace Persistence.Extensions;

public static class ServiceCollectionExtension
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddPersistenceServices(IConfiguration configuration)
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

        public void AddRepositories()
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductGroupRepository, ProductGroupRepository>();
            services.AddScoped<IProductSizeRepository, ProductSizeRepository>();
            services.AddScoped<IStorageService, MinioStorageService>();
            services.AddScoped<IPassedEventRepository, PassedEventRepository>();
            services.AddScoped<IPassedEventCostRepository, PassedEventCostRepository>();
            services.AddScoped<ITransactionRepository, TransactionRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IOrderAuditRepository, OrderAuditRepository>();
            services.AddScoped<IOrderTransactionsRepository, OrderTransactionRepository>();
        }

        public void AddInterceptors()
        {
            services.AddScoped<IInterceptor, SetEntityDetailsInterceptor>();
        }

        public void AddInitiaizers()
        {
            services.AddScoped<IDataInitializer, ProductGroupInitializer>();
            services.AddScoped<IDataInitializer, PassedEventCostInitializer>();
        }

        public void AddDatabaseManagers()
        {
            services.AddScoped<IDatabaseTransactionManager, DatabaseTransactionManager>();
        }
    }
}
