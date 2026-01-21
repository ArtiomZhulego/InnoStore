using Application.Abstractions.DTOs.Entities;
using Application.Abstractions.Services;
using Application.Mappers;
using Domain.Abstractions;
using Microsoft.Extensions.Logging;

namespace Application.Services;

internal class PassedEventService(
    IPassedEventRepository passedEventRepository,
    ILogger<PassedEventService> logger
) : IPassedEventService
{
    public async Task SavePassedEventIdempotentAsync(PassedEventDTO passedEventDTO, CancellationToken cancellationToken)
    {
        var doesEventExist = await passedEventRepository.AnyAsync(passedEventDTO.EventId, cancellationToken);

        if (doesEventExist)
        {
            logger.LogInformation("Event with ID {PassedEventId} already exists. Skipping save operation.", passedEventDTO.EventId);
            return;
        }

        var passedEvent = passedEventDTO.ToPassedEvent();

        await passedEventRepository.AddAsync(passedEvent, cancellationToken);
    }
}
