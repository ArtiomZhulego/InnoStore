using Application.Abstractions.DTOs.Entities;
using Application.Abstractions.Services;
using Microsoft.Extensions.Logging;
using Presentation.Exceptions;

namespace Presentation.Handlers;

public class PassedEventHandler(
    ILogger<PassedEventHandler> logger,
    IPassedEventService passedEventService
)
{
    public async Task Handle(PassedEventDTO passedEvent)
    {
        try
        {
            await passedEventService.SavePassedEventIdempotentAsync(passedEvent);
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Error handling passed event with ID {PassedEventId}", passedEvent.Id);
            throw new PassedEventException($"Error handling passed event with ID {passedEvent.Id}", exception);
        }
    }
}