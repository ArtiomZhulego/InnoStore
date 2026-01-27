using Application.Abstractions.DTOs.Clients.HRM;
using Application.Abstractions.DTOs.Clients.HRM.Employees;
using Application.Abstractions.Services;
using Application.Contsants;
using Application.Mappers;
using Domain.Abstractions;
using Microsoft.Extensions.Logging;
using Quartz;

namespace Application.BackgroundJobs;

//Smart HRM system let us get only 2000 employees once, we need to send at least 2 queries. 
public sealed class EmployeeSearchJob(IEmployeeService employeeService, IUserRepository userRepository, ILogger<EmployeeSearchJob> logger) : IJob
{
    private const int PageSize = 2000;
    
    public async Task Execute(IJobExecutionContext context)
    {
        logger.LogInformation("Starting {JobName} at {Time}", nameof(EmployeeSearchJob), DateTime.UtcNow);

        try
        {
            var searchRequest = new EmployeeSearchRequest
            {
                DismissalStatus = new DismissalStatusFilter
                {
                    Equals = EmployeeConstants.DismissalStatus_Actual
                },
                EmployeeManagers = []
            };
            
            var allEmployees = await employeeService.LoadEmployeesAsync(searchRequest, PageSize, CancellationToken.None);

            var newEmployees = await GetNewEmployeesAsync(allEmployees);

            var users = newEmployees.ToUsers();
            
            await userRepository.AddRangeAsync(users, CancellationToken.None);
            
            logger.LogInformation("{JobName} executed successfully. Total employees retrieved: {Total}, New employees added: {NewCount}.",
                nameof(EmployeeSearchJob),
                allEmployees.Count,
                newEmployees.Count);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while executing {JobName}", nameof(EmployeeSearchJob));
        }
    }
    
    private async Task<List<EmployeeModel>> GetNewEmployeesAsync(List<EmployeeModel> allEmployees)
    {
        var existingEmployeeIds = await userRepository.GetUserHrmIdsAsync(CancellationToken.None);

        return allEmployees
            .Where(e => !existingEmployeeIds.Contains(e.Id))
            .ToList();
    }
}