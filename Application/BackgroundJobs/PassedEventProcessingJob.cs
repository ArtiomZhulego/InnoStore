using Application.Abstractions.DTOs.Clients.HRM;
using Application.Abstractions.DTOs.Clients.HRM.Employees;
using Application.Abstractions.Services;
using Application.Contsants;
using Application.Mappers;
using Domain.Abstractions;
using Domain.Common;
using Domain.Entities;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Quartz;

namespace Application.BackgroundJobs;

public sealed class PassedEventProcessingJob(
    IPassedEventRepository passedEventRepository,
    IUserRepository userRepository,
    IPassedEventCostRepository passedEventCostRepository,
    ITransactionRepository transactionRepository,
    IEmployeeService employeeService,
    ITransactionManager transactionManager,
    IStringLocalizer stringLocalizer,
    ILogger<PassedEventProcessingJob> logger
) : IJob
{
    private const int PassedEventPageSize = 20;

    private const int EmployeesPageSize = 100;

    private const int ProcessingDurationInMilliseconds = 10000;

    public async Task Execute(IJobExecutionContext context)
    {
        var pageNumber = 0;

        var cancellationTokenSource = new CancellationTokenSource(ProcessingDurationInMilliseconds);

        var cancellationToken = cancellationTokenSource.Token;

        var unprocessedEvents = await GetUnprocessedEventsAsync(PassedEventPageSize, pageNumber, cancellationToken);
        var presentUserHrmIds = await userRepository.GetUserHrmIdsAsync(cancellationToken);

        while (unprocessedEvents.Any())
        {
            try
            {
                await transactionManager.BeginAsync(cancellationToken);
                await EnsureAllParticipantsArePresentAsync(unprocessedEvents, presentUserHrmIds, cancellationToken);
                await transactionManager.CommitAsync(cancellationToken);

                await transactionManager.BeginAsync(cancellationToken);
                await ProcessEventsAsync(unprocessedEvents, presentUserHrmIds, cancellationToken);
                await transactionManager.CommitAsync(cancellationToken);
            }
            catch
            {
                await transactionManager.RollbackAsync(cancellationToken);
                throw;
            }

            ++pageNumber;
            unprocessedEvents = await GetUnprocessedEventsAsync(PassedEventPageSize, pageNumber, cancellationToken);
        }
    }

    private async Task<PassedEvent[]> GetUnprocessedEventsAsync(int pageSize, int pageNumber, CancellationToken cancellationToken)
    {
        var page = new Page
        {
            Size = PassedEventPageSize,
            Number = pageNumber
        };

        var unprocessedEvents = await passedEventRepository.GetAllUnprocessedAsync(page, cancellationToken);

        return unprocessedEvents;
    }

    private async Task ProcessEventsAsync(PassedEvent[] passedEvents, IEnumerable<int> presentUsersHrmIds, CancellationToken cancellationToken)
    {
        var transactions = new List<Transaction>();

        var costs = await passedEventCostRepository.GetAllAsync(cancellationToken);

        var descriptionFormat = stringLocalizer["ParticipatingInEvent_MessageFormat"];

        foreach (var passedEvent in passedEvents)
        {
            var cost = costs.FirstOrDefault(x => x.EventType == passedEvent.EventType);

            if (cost is null)
            {
                throw new Exception($"Cost is not present for {passedEvent.EventType.ToString()}.");
            }

            var participantsHrmIds = passedEvent.Participants
                .Select(x => x.HrmId)
                .ToArray();

            var passedEventUsers = await userRepository.GetAllByHrmIdsAsync(participantsHrmIds, cancellationToken);

            foreach (var user in passedEventUsers)
            {
                var transaction = new Transaction
                {
                    Id = Guid.NewGuid(),
                    Description = string.Format(descriptionFormat, passedEvent.Name),
                    Type = TransactionType.ParticipatingInEvent,
                    UserId = user.Id!.Value,
                    Amount = cost.Amount,
                };

                transactions.Add(transaction);
            }
        }

        await transactionRepository.AddRangeAsync(transactions, cancellationToken);
        var processedEventIds = passedEvents.Select(x => x.Id).ToArray();
        await passedEventRepository.MarkAsProcessedAsync(processedEventIds, cancellationToken);
    }

    private async Task EnsureAllParticipantsArePresentAsync(PassedEvent[] events, IEnumerable<int> presentUsersHrmIds, CancellationToken cancellationToken)
    {
        var participants = events.SelectMany(x => x.Participants).ToArray();

        var missingParticipants = participants
            .Where(x => !presentUsersHrmIds.Contains(x.HrmId))
            .Distinct()
            .ToArray();

        if (!missingParticipants.Any())
        {
            return;
        }

        var emails = missingParticipants.Select(x => x.Email)
            .ToArray();

        var searchRequest = GetEmployeeSearchRequest(emails);

        var employees = await employeeService.LoadEmployeesAsync(searchRequest, EmployeesPageSize, cancellationToken);

        var users = employees.ToUsers();

        await userRepository.AddRangeAsync(users, cancellationToken);

        LogAddedUsers(users);
    }

    private EmployeeSearchRequest GetEmployeeSearchRequest(string[] emails)
    {
        var searchRequest = new EmployeeSearchRequest
        {
            DismissalStatus = new DismissalStatusFilter
            {
                Equals = EmployeeConstants.DismissalStatus_Actual
            },
            EmployeeManagers = [],
            Email = new EmailFilter
            {
                In = emails
            }
        };

        return searchRequest;
    }

    private void LogAddedUsers(IEnumerable<User> users)
    {
        foreach (var user in users)
        {
            logger.LogInformation("Added user: HrmId={HrmId}, Email={Email}", user.HrmId, user.Email);
        }
    }
}
