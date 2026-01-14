using Domain.Abstractions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

internal sealed class PassedEventRepository(InnoStoreContext context) : IPassedEventRepository
{
    public async Task AddAsync(PassedEvent passedEvent, CancellationToken cancellationToken)
    {
        context.Set<PassedEvent>().Add(passedEvent);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> AnyAsync(Guid id, CancellationToken cancellationToken)
    {
        var result = await context.Set<PassedEvent>()
            .AsQueryable()
            .AnyAsync(x => x.Id == id, cancellationToken);

        return result;
    }
}
