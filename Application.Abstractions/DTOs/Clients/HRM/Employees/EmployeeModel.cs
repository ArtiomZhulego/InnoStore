using System.Text.Json.Serialization;

namespace Application.Abstractions.DTOs.Clients.HRM.Employees;

public sealed class EmployeeModel
{
    [JsonPropertyName("id")]
    public int Id { get; init; }
    
    [JsonPropertyName("firstNameRu")]
    public string? FirstNameRU { get; init; }
    
    [JsonPropertyName("birthDate")]
    public DateOnly? Birthdate { get; init; }
    
    [JsonPropertyName("patronymicRu")]
    public string? PatronymicRU { get; init; }
    
    [JsonPropertyName("lastNameRu")]
    public string? LastNameRU { get; init; }
    
    [JsonPropertyName("firstNameEn")]
    public string? FirstNameEN { get; init; }
    
    [JsonPropertyName("patronymicEn")]
    public string? PatronymicEN { get; init; }
    
    [JsonPropertyName("lastNameEn")]
    public string? LastNameEN { get; init; }

    [JsonPropertyName("email")]
    public required string Email { get; init; }

    [JsonPropertyName("officeActualId")]
    public int? OfficeId { get; init; }
    
    [JsonPropertyName("jobTitleId")]
    public Guid? JobTitleId { get; init; }
    
    [JsonPropertyName("linkProfilePictureMini")]
    public string? LinkProfilePictureMini { get; set; }
}