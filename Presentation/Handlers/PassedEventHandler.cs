using Application.Abstractions.DTOs.Entities.PassedEvent.V1;
using Application.Abstractions.Services;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Presentation.Constants;
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
            var eventVersion = GetEventVersion(message);

            switch (eventVersion)
            {
                case EventVersions.V1:
                    await HandleEventV1Async(message);
                    break;
                default:
                    throw new Exception($"Unsupported event version: {eventVersion}");
            }
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Error handling passed event.");
            throw new PassedEventException($"Error handling passed event.", exception);
        }
    }

    private string GetEventVersion(string message)
    {
        var jsonObject = JObject.Parse(message);
        return jsonObject["EventVersion"]?.ToString() ?? string.Empty;
    }

    private async Task HandleEventV1Async(string message)
    {
        var passedEvent = JsonConvert.DeserializeObject<PassedEventDTO>(message);

        if (passedEvent is null)
        {
            throw new Exception($"Event deserialization for version {EventVersions.V1} resulted in null object.");
        }

        await passedEventService.SavePassedEventIdempotentAsync(passedEvent);
    }
}