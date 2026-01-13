using Application.Abstractions.DTOs.Entities;

namespace Application.Abstractions.Services;

public interface IPassedEventService
{
    Task SavePassedEventAsync(PassedEventDTO passedEvent, CancellationToken cancellationToken);
}
