using System.Diagnostics.CodeAnalysis;
using Application.Clients.HRM.Abstractions;
using Application.Handlers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Http.Resilience;
using Polly;
using Refit;

namespace Application.Clients.HRM;

[ExcludeFromCodeCoverage]
public static class HrmDependencies
{
    public static IServiceCollection AddHrm(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddRefitClient<IAuthApiClient>()
                .ConfigureHttpClient(c => c.BaseAddress = new Uri(configuration?.GetValue<string>("HRM:KeycloakUrl") ?? string.Empty))
                .AddResilienceHandler("auth-client-pipeline", pipeline =>
                {
                    pipeline.AddRetry(new HttpRetryStrategyOptions
                    {
                        MaxRetryAttempts = 3,
                        BackoffType = DelayBackoffType.Exponential,
                        UseJitter = true
                    });
                }); ;

        services.AddRefitClient<IEmployeeApiClient>()
                .ConfigureHttpClient(c =>
                {
                    c.BaseAddress = new Uri(configuration.GetValue<string>("HRM:EmployeeApiUrl") ?? string.Empty);
                })
                .AddHttpMessageHandler(provider => new AuthTokenHandler(provider, configuration))
                .AddResilienceHandler("employee-client-pipeline", pipeline =>
                {
                    pipeline.AddRetry(new HttpRetryStrategyOptions
                    {
                        MaxRetryAttempts = 3,
                        BackoffType = DelayBackoffType.Exponential,
                        UseJitter = true,
                    });
                });

        return services;
    }
}