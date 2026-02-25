using Application.Abstractions.DTOs.Entities.PassedEvent.V1;

namespace Application.Abstractions.Services;

public interface IPassedEventService
{
    Task SavePassedEventIdempotentAsync(PassedEventDTO passedEventDto, CancellationToken cancellationToken = default);
}
