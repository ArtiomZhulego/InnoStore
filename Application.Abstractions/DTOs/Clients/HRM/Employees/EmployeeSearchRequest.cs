namespace Application.Abstractions.DTOs.Clients.HRM.Employees;

public sealed class EmployeeSearchRequest
{
    public DismissalStatusFilter? DismissalStatus { get; set; }

    public EmailFilter? Email { get; set; }

    public string[] EmployeeManagers { get; set; } = [];
}