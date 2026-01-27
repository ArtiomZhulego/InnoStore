using Domain.Abstractions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

public sealed class PassedEventCostRepository(InnoStoreContext context) : IPassedEventCostRepository
{
    public async Task<PassedEventCost[]> GetAllAsync(CancellationToken cancellationToken)
    {
        var costs = await context.PassedEventCosts
            .AsNoTracking()
            .ToArrayAsync(cancellationToken);

        return costs;
    }
}
