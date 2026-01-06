using Serilog;

namespace InnoStore.Extensions;

public static class ServiceCollectionExtension
{
    public static void ConfigureCors(this IServiceCollection services, IConfiguration configuration)
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

    public static void ConfigureLogger(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSerilog((context, loggerConfiguration) =>
        {
            loggerConfiguration
                .Enrich.FromLogContext()
                .WriteTo.Console(Serilog.Events.LogEventLevel.Information);
        });
    }
}
