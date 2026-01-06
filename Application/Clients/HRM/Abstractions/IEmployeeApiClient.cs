using Application.Abstractions.DTOs.Clients.HRM.Employees;
using Refit;

namespace Application.Clients.HRM.Abstractions;

public interface IEmployeeApiClient
{
    [Post("/api/employee-management/api/v2/employees/search")]
    Task<EmployeeSearchResult> GetEmployeesAsync(
        [Body] EmployeeSearchRequest searchRequest,
        [Query] int page = 0,
        [Query] int size = 50,
        [Query(CollectionFormat.Multi)] string[] sort = null);
}