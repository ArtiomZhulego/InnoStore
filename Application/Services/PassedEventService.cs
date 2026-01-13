using Application.Abstractions.DTOs.Entities;
using Application.Abstractions.Services;

namespace Application.Services;

internal class PassedEventService : IPassedEventService
{
    public Task SavePassedEventAsync(PassedEventDTO passedEvent, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
