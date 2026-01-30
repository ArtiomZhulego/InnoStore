using Domain.Entities;

namespace Domain.Abstractions;

public interface IPassedEventCostRepository
{
    Task<PassedEventCost[]> GetAllAsync(CancellationToken cancellationToken);
}
