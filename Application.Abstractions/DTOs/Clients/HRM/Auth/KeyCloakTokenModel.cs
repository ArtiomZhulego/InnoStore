using System.Text.Json.Serialization;

namespace Application.Abstractions.DTOs.Clients.HRM.Auth;

public sealed class KeyCloakTokenModel
{
    [JsonPropertyName("access_token")]
    public string AccessToken { get; set; }
}