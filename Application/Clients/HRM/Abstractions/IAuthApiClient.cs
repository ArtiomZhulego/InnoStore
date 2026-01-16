using Application.Abstractions.DTOs.Clients.HRM.Auth;
using Refit;

namespace Application.Clients.HRM.Abstractions;

public interface IAuthApiClient
{
    [Post("/auth/realms/innowise-group/protocol/openid-connect/token")]
    Task<KeyCloakTokenModel> GetTokenAsync([Body(BodySerializationMethod.UrlEncoded)] KeyCloakTokenRequestParameters request);
}