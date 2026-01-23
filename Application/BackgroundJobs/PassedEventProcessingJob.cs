using Application.Mappers;
using Domain.Abstractions;
using Domain.Common;
using Domain.Entities;
using Microsoft.Extensions.Logging;
using Quartz;

namespace Application.BackgroundJobs;

public sealed class PassedEventProcessingJob(
    IPassedEventRepository passedEventRepository,
    IUserRepository userRepository,
    IPassedEventCostRepository passedEventCostRepository,
    ITransactionRepository transactionRepository,
    IDatabaseTransactionManager transactionManager,
    ILogger<PassedEventProcessingJob> logger
) : IJob
{
    private const int PassedEventPageSize = 20;

    private const int ProcessingDurationInMilliseconds = 1000000000;

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

        var cancellationTokenSource = new CancellationTokenSource(ProcessingDurationInMilliseconds);

        var cancellationToken = cancellationTokenSource.Token;

        var unprocessedEvents = await GetUnprocessedEventsAsync(PassedEventPageSize, pageNumber, cancellationToken);
        var presentUserHrmIds = await userRepository.GetUserHrmIdsAsync(cancellationToken);

        while (unprocessedEvents.Any())
        {
            try
            {
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
                throw new Exception("Not all participants are present in the system.");
            }

            foreach (var user in passedEventUsers)
            {
                var transaction = new Transaction
                {
                    Id = Guid.NewGuid(),
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
}
