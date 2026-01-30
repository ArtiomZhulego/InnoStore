using Application.Abstractions.DTOs.Clients.HRM.Employees;
using Application.Abstractions.Services;
using Application.Clients.HRM.Abstractions;

namespace Application.Services;

public sealed class EmployeeService(IEmployeeApiClient employeeApiClient) : IEmployeeService
{
    public async Task<List<EmployeeModel>> LoadEmployeesAsync(EmployeeSearchRequest request, int pageSize, CancellationToken cancellationToken = default)
    {
        var result = new List<EmployeeModel>();
        var index = 0;
        var hasMoreItems = true;

        string[] sort = { "lastNameRu,asc", "firstNameRu,asc" };

        while (hasMoreItems)
        {
            var pageResult = await employeeApiClient.GetEmployeesAsync(request, index, pageSize, sort);

            result.AddRange(pageResult.Content);

            if (pageResult.Content.Count < pageSize)
            {
                hasMoreItems = false;
            }

            index++;
        }

        return result;
    }
}
