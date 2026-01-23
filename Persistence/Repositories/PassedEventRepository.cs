using Domain.Abstractions;
using Domain.Common;
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

    public async Task<bool> AnyAsync(Guid eventId, CancellationToken cancellationToken)
    {
        var result = await context.Set<PassedEvent>()
            .AsQueryable()
            .AnyAsync(x => x.EventId == eventId, cancellationToken);

        return result;
    }

    public async Task<PassedEvent[]> GetAllUnprocessedAsync(Page page, CancellationToken cancellationToken)
    {
        var unprocessedEvents = await context.PassedEvents.AsQueryable()
            .Where(x => !x.IsProcessed)
            .Skip(page.Size * page.Number)
            .Take(page.Size)
            .Include(x => x.Participants)
            .ToArrayAsync(cancellationToken);

        return unprocessedEvents;
    }

    public async Task MarkAsProcessedAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken)
    {
        await context.PassedEvents
            .Where(x => ids.Contains(x.Id))
            .ExecuteUpdateAsync(setters => setters.SetProperty(x => x.IsProcessed, true));
    }
}
