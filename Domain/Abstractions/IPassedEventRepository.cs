using Domain.Entities;

namespace Domain.Abstractions;

public interface IPassedEventRepository
{
    Task<bool> AnyAsync(Guid id, CancellationToken cancellationToken);

    Task AddAsync(PassedEvent passedEvent, CancellationToken cancellationToken);
}