using Application.Abstractions.DTOs.Clients.HRM.Employees;

namespace Application.Abstractions.Services;

public interface IEmployeeService
{
    Task<List<EmployeeModel>> LoadEmployeesAsync(EmployeeSearchRequest request, int pageSize, CancellationToken cancellationToken = default);
}
