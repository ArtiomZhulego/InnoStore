using System.Text.Json.Serialization;

namespace Application.Abstractions.DTOs.Clients.HRM.Employees;

public sealed class EmployeeSearchResult
{
    [JsonPropertyName("totalElements")]
    public required int TotalElements { get; init; }

    [JsonPropertyName("content")]
    public required ICollection<EmployeeModel> Content { get; init; }
}