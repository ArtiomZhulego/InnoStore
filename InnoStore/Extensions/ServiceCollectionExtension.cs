using Amazon;
using Amazon.S3;
using Application.Abstractions.StorageAggregate;
using Application.Settings;
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
        public async Task<IApplicationBuilder> ApplyMigrations()
        {
            using var scope = app.ApplicationServices.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<InnoStoreContext>();
            await dbContext.Database.MigrateAsync();

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

        public async Task<IApplicationBuilder> ApplyBlobStorageInitialization()
        {
            using var scope = app.ApplicationServices.CreateScope();
            var storageService = scope.ServiceProvider.GetRequiredService<IStorageService>();
            await storageService.EnsureBucketExistsAsync();
            return app;
        }
    }
}
