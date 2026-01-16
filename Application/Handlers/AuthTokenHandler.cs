using System.Net.Http.Headers;
using Application.Abstractions.DTOs.Clients.HRM.Auth;
using Application.Clients.HRM;
using Application.Clients.HRM.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Handlers;

public sealed class AuthTokenHandler(IServiceProvider provider, IConfiguration configuration) : DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var authClient = provider.GetRequiredService<IAuthApiClient>();
        var tokenResponse = await authClient.GetTokenAsync(new KeyCloakTokenRequestParameters
        {
            ClientId = configuration.GetValue<string>("HRM:ClientId")!,
            Username = configuration.GetValue<string>("HRM:Username")!,
            Password = configuration.GetValue<string>("HRM:Password")!,
        });

        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", tokenResponse.AccessToken);

        return await base.SendAsync(request, cancellationToken);
    }
}