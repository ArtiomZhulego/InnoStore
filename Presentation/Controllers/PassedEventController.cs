using Application.Abstractions.DTOs.Entities.PassedEvent.V1;
using Application.Abstractions.Services;
using Microsoft.AspNetCore.Mvc;
using Presentation.Constants;

namespace Presentation.Controllers;

[ApiController]
[Route(PathConstants.PassedEvents.Controller)]
public class PassedEventController(IPassedEventService passedEventService) : ControllerBase
{
    [HttpPost(PathConstants.PassedEvents.Save)]
    public async Task<IActionResult> SavePassedEventV1Async(PassedEventDTO passedEventDTO, CancellationToken cancellationToken)
    {
        await passedEventService.SavePassedEventIdempotentAsync(passedEventDTO, cancellationToken);
        return Ok();
    }
}
