using Application.Abstractions.DTOs.Clients.HRM;
using Application.Abstractions.DTOs.Clients.HRM.Employees;
using Application.Clients.HRM;
using Application.Clients.HRM.Abstractions;
using Application.Mappers;
using Domain.Abstractions;
using Microsoft.Extensions.Logging;
using Quartz;

namespace Application.BackgroundJobs;

//Smart HRM system let us get only 2000 employees once, we need to send at least 2 queries. 
public sealed class EmployeeSearchJob(IEmployeeApiClient employeeApiClient, IUserRepository userRepository, ILogger<EmployeeSearchJob> logger) : IJob
{
    private bool HasMoreItems { get; set; } = true;

    private int Index { get; set; }
    
    private const int PageSize = 2000;
    
    public async Task Execute(IJobExecutionContext context)
    {
        logger.LogInformation("Starting {JobName} at {Time}", nameof(EmployeeSearchJob), DateTime.UtcNow);

        try
        {
            var searchRequest = new EmployeeSearchRequest
            {
                DismissalStatus = new DismissalStatusFilter { Equals = "ACTUAL" },
                EmployeeManagers = []
            };

            string[] sort = { "lastNameRu,asc", "firstNameRu,asc" };
            var allEmployees = new List<EmployeeModel>();
            
            while (HasMoreItems)
            {
                var pageResult = await employeeApiClient.GetEmployeesAsync(searchRequest, Index, PageSize, sort);
            
                allEmployees.AddRange(pageResult.Content);
                
                if (pageResult.Content.Count < PageSize)
                {
                    HasMoreItems = false;
                }
                
                Index++;
            }

            var existingEmployeeIds = await userRepository.GetUserIdsAsync(CancellationToken.None);

            var newEmployees = allEmployees
                .Where(e => !existingEmployeeIds.Contains(e.Id))
                .ToList();

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
}