using Domain.Abstractions;
using Domain.Common;
using Domain.Entities;
using Quartz;

namespace Application.BackgroundJobs;

public sealed class PassedEventProcessingJob(
    IPassedEventRepository passedEventRepository,
    ITransactionManager transactionManager
) : IJob
{
    private const int PageSize = 20;
    private const int ProcessingDurationInMilliseconds = 10000;

    public async Task Execute(IJobExecutionContext context)
    {
        var pageNumber = 0;

        var cancellationTokenSource = new CancellationTokenSource(ProcessingDurationInMilliseconds);

        var cancellationToken = cancellationTokenSource.Token;

        var unprocessedEvents = await GetUnprocessedEventsAsync(PageSize, pageNumber, cancellationToken);

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
            unprocessedEvents = await GetUnprocessedEventsAsync(PageSize, pageNumber, cancellationToken);
        }
    }

    private async Task<PassedEvent[]> GetUnprocessedEventsAsync(int pageSize, int pageNumber, CancellationToken cancellationToken)
    {
        var page = new Page
        {
            Size = PageSize,
            Number = pageNumber
        };

        var unprocessedEvents = await passedEventRepository.GetAllUnprocessedAsync(page, cancellationToken);

        return unprocessedEvents;
    }

    private async Task ProcessEventsAsync(PassedEvent[] passedEvents, CancellationToken cancellationToken)
    {
        var participants = passedEvents.SelectMany(x => x.Participants).ToArray();

        foreach (var participant in participants)
        {
            var transaction = new Transaction
            {
                Id = Guid.NewGuid(),
                Type = TransactionType.ParticipatingInEvent,
                Description = "Какое-то описание",
                Amount = 10.0m,
                UserId = Guid.NewGuid(),
            };
        }

        var processedEventIds = passedEvents.Select(x => x.Id).ToArray();
        await passedEventRepository.MarkAsProcessedAsync(processedEventIds, cancellationToken);
    }
}
