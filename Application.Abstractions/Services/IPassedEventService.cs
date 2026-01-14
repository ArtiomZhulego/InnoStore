using Application.Abstractions.DTOs.Entities;

namespace Application.Abstractions.Services;

public interface IPassedEventService
{
    Task SavePassedEventIdempotentAsync(PassedEventDTO passedEvent, CancellationToken cancellationToken = default);
}
