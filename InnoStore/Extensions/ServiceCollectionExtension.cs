using Amazon;
using Amazon.S3;
using Domain.Abstractions;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.DataInitializers.Abstractions;
using Persistence.Settings;
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

        public void ConfigureMinio(IConfiguration configuration)
        {
            services.Configure<MinioStorageSettings>(configuration.GetSection(MinioStorageSettings.ConfigurationSection));

            var options = configuration.GetSection(MinioStorageSettings.ConfigurationSection).Get<MinioStorageSettings>() ?? throw new ArgumentNullException($"section {MinioStorageSettings.ConfigurationSection} are not configured properly.");
            
            services.AddSingleton<IAmazonS3>(sp =>
            {
                var config = new AmazonS3Config
                {
                    RegionEndpoint = RegionEndpoint.GetBySystemName("us-east-1"),
                    UseHttp = true,
                    ServiceURL = options.SpaceUrl,
                    ForcePathStyle = true,
                };

                return new AmazonS3Client(
                    options.AccessKey,
                    options.SecretKey,
                    config);
            });
        }
    }

    extension(IApplicationBuilder app)
    {
        public async Task<IApplicationBuilder> ExecuteActionsBeforeStart()
        {
            using var scope = app.ApplicationServices.CreateScope();
            await app.ApplyMigrations(scope.ServiceProvider);
            await app.ApplyDataInitializers(scope.ServiceProvider);
            await app.ApplyBlobStorageInitialization(scope.ServiceProvider);
            return app;
        }
        public async Task<IApplicationBuilder> ApplyMigrations(IServiceProvider serviceProvider)
        {
            var dbContext = serviceProvider.GetRequiredService<InnoStoreContext>();
            await dbContext.Database.MigrateAsync();

            return app;
        }

        public async Task<IApplicationBuilder> ApplyDataInitializers(IServiceProvider serviceProvider)
        {
            var dataInitializers = serviceProvider.GetServices<IDataInitializer>();
            foreach (var initializer in dataInitializers)
            {
                await initializer.InitializeAsync();
            }

            return app;
        }

        public async Task<IApplicationBuilder> ApplyBlobStorageInitialization(IServiceProvider serviceProvider)
        {
            var storageService = serviceProvider.GetRequiredService<IStorageService>();
            await storageService.EnsureBucketExistsAsync();
            return app;
        }
    }
}
