using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.DataInitializers.Abstractions;
using Serilog;

namespace InnoStore.Extensions;

public static class ServiceCollectionExtension
{
    extension(IServiceCollection services)
    {
        public void ConfigureCors(IConfiguration configuration)
        {
            services.AddCors(opt =>
            {
                opt.AddDefaultPolicy(policy =>
                {
                    policy.AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowAnyOrigin();
                });
            });
        }

        public void ConfigureLogger(IConfiguration configuration)
        {
            services.AddSerilog((context, loggerConfiguration) =>
            {
                loggerConfiguration
                    .Enrich.FromLogContext()
                    .WriteTo.Console(Serilog.Events.LogEventLevel.Information);
            });
        }
    }

    extension(IApplicationBuilder app)
    {
        public IApplicationBuilder ApplyMigrations()
        {
            using var scope = app.ApplicationServices.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<InnoStoreContext>();
            dbContext.Database.Migrate();

            return app;
        }

        public async Task<IApplicationBuilder> ApplyDataInitializers()
        {
            using var scope = app.ApplicationServices.CreateScope();
            var dataInitializers = scope.ServiceProvider.GetServices<IDataInitializer>();
            foreach (var initializer in dataInitializers)
            {
                await initializer.InitializeAsync();
            }

            return app;
        }
    }
}
