using System.Text.Json.Serialization;

namespace Application.Abstractions.DTOs.Clients.HRM.Auth;

public sealed class KeyCloakTokenRequestParameters
{
    [JsonPropertyName("grant_type")]
    public string GrantType { get; } = "password";

    [JsonPropertyName("client_id")]
    public required string ClientId { get; set; }

    [JsonPropertyName("username")]
    public required string Username { get; set; }

    [JsonPropertyName("password")]
    public required string Password { get; set; }
}