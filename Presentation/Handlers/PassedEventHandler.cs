using Application.Abstractions.DTOs.Entities;
using Application.Abstractions.Services;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Presentation.Exceptions;
using Wolverine;
using Wolverine.Attributes;

namespace Presentation.Handlers;

[WolverineHandler]
public class PassedEventHandler(
    IPassedEventService passedEventService,
    ILogger<PassedEventHandler> logger
) : IWolverineHandler
{
    public async Task HandleAsync(string message)
    {
        try
        {
            var passedEvent = JsonConvert.DeserializeObject<PassedEventDTO>(message);

            if (passedEvent is null)
            {
                throw new Exception("Passed event deserialization resulted in null object.");
            }

            await passedEventService.SavePassedEventIdempotentAsync(passedEvent);
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Error handling passed event.");
            throw new PassedEventException($"Error handling passed event.", exception);
        }
    }
}