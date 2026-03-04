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
    public async Task SavePassedEventIdempotentAsync(PassedEventDTO passedEventDto, CancellationToken cancellationToken = default)
    {
        var doesEventExist = await passedEventRepository.AnyAsync(passedEventDto.EventContent.Id, cancellationToken);

        if (doesEventExist)
        {
            logger.LogInformation("Event with ID {PassedEventId} already exists. Skipping save operation.", passedEventDto.EventContent.Id);
            return;
        }

        var passedEvent = passedEventDto.ToPassedEvent();

        await using var transaction = await databaseTransactionManager.BeginTransactionAsync(cancellationToken);
        await passedEventRepository.AddAsync(passedEvent, cancellationToken);
        await transaction.CommitAsync(cancellationToken);
    }
}
