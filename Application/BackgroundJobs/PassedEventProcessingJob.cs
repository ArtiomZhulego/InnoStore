using Application.Abstractions.Options;
using Application.Mappers;
using Domain.Abstractions;
using Domain.Common;
using Domain.Entities;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Quartz;

namespace Application.BackgroundJobs;

public sealed class PassedEventProcessingJob(
    IPassedEventRepository passedEventRepository,
    IUserRepository userRepository,
    IPassedEventCostRepository passedEventCostRepository,
    ITransactionRepository transactionRepository,
    IDatabaseTransactionManager transactionManager,
    IOptions<PassedEventProcessingJobOptions> options,
    ILogger<PassedEventProcessingJob> logger
) : IJob
{
    private static SemaphoreSlim SemaphoreSlim = new SemaphoreSlim(1, 1);

    public async Task Execute(IJobExecutionContext context)
    {
        await SemaphoreSlim.WaitAsync();

        try
        {
            await ExecutePassedEventsProcessingAsync();
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Error during processing batch of passed events.");
        }
        finally
        {
            SemaphoreSlim.Release();
        }
    }

    private async Task ExecutePassedEventsProcessingAsync()
    {
        var pageNumber = 0;

        var cancellationTokenSource = new CancellationTokenSource(options.Value.PassedEventProcessingJobDurationInMilliseconds);

        var cancellationToken = cancellationTokenSource.Token;

        var unprocessedEvents = await GetUnprocessedEventsAsync(options.Value.PassedEventProcessingBatchCount, pageNumber, cancellationToken);

        while (unprocessedEvents.Any())
        {
            try
            {
                await transactionManager.BeginAsync(cancellationToken);
                await ProcessEventsAsync(unprocessedEvents, cancellationToken);
                await transactionManager.CommitAsync(cancellationToken);
            }
            catch
            {
                await transactionManager.RollbackAsync(cancellationToken);
                throw;
            }

            ++pageNumber;
            unprocessedEvents = await GetUnprocessedEventsAsync(options.Value.PassedEventProcessingBatchCount, pageNumber, cancellationToken);
        }
    }

    private async Task<PassedEvent[]> GetUnprocessedEventsAsync(int pageSize, int pageNumber, CancellationToken cancellationToken)
    {
        var page = new Page
        {
            Size = pageSize,
            Number = pageNumber
        };

        var unprocessedEvents = await passedEventRepository.GetUnprocessedAsync(page, cancellationToken);

        return unprocessedEvents;
    }

    private async Task ProcessEventsAsync(PassedEvent[] passedEvents, CancellationToken cancellationToken)
    {
        var transactions = new List<Transaction>();

        var costs = await passedEventCostRepository.GetAllAsync(cancellationToken);

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

            if (passedEventUsers.Length != participantsHrmIds.Length)
            {
                var notFoundUsersHrmIds = GetNotFoundUsersHrmIds(passedEventUsers, participantsHrmIds);
                LogNotFoundUsers(notFoundUsersHrmIds);

                throw new Exception($"Participants with HRM IDs [{string.Join(", ", notFoundUsersHrmIds)}] are not found in the system.");
            }

            foreach (var user in passedEventUsers)
            {
                var transaction = new Transaction
                {
                    Id = Guid.NewGuid(),
                    Type = TransactionType.AddForParticipatingInEvent,
                    UserId = user.Id!.Value,
                    Amount = cost.Amount,
                };

                transactions.Add(transaction);
            }
        }

        await transactionRepository.AddRangeAsync(transactions, cancellationToken);
        var processedEventIds = passedEvents.Select(x => x.Id);
        await passedEventRepository.MarkAsProcessedAsync(processedEventIds, cancellationToken);
    }

    private int[] GetNotFoundUsersHrmIds(User[] foundUsers, int[] participantsHrmIds)
    {
        var foundUsersHrmIds = foundUsers.Select(x => x.HrmId).ToArray();

        var notFoundUsersHrmIds = participantsHrmIds
            .Where(participantHrmId => !foundUsersHrmIds.Contains(participantHrmId))
            .ToArray();

        return notFoundUsersHrmIds;
    }

    private void LogNotFoundUsers(int[] hrmIds)
    {
        foreach (var hrmId in hrmIds)
        {
            logger.LogError("User with HRM ID {HrmId} not found in the system.", hrmId);
        }
    }
}
