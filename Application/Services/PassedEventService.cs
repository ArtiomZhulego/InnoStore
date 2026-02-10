using Application.Abstractions.DTOs.Entities.PassedEvent.V1;
using Application.Abstractions.Services;
using Application.Extensions;
using Application.Mappers;
using Application.Mappers.PassedEvent.V1;
using Domain.Abstractions;
using Microsoft.Extensions.Logging;

namespace Application.Services;

internal class PassedEventService(
    IPassedEventRepository passedEventRepository,
    IDatabaseTransactionManager databaseTransactionManager,
    ILogger<PassedEventService> logger
) : IPassedEventService
{
    public async Task SavePassedEventIdempotentAsync(PassedEventDTO passedEventDTO, CancellationToken cancellationToken)
    {
        var doesEventExist = await passedEventRepository.AnyAsync(passedEventDTO.EventContent.Id, cancellationToken);

        if (doesEventExist)
        {
            logger.LogInformation("Event with ID {PassedEventId} already exists. Skipping save operation.", passedEventDTO.EventContent.Id);
            return;
        }

        var passedEvent = passedEventDTO.ToPassedEvent();

        await databaseTransactionManager.BeginAsync();

        try
        {
            await passedEventRepository.AddAsync(passedEvent, cancellationToken);
            await databaseTransactionManager.CommitAsync();
        }
        catch
        {
            await databaseTransactionManager.RollbackAsync();
            throw;
        }
    }
}
