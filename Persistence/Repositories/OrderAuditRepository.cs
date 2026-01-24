using Domain.Abstractions;
using Domain.Entities;

namespace Persistence.Repositories;

internal sealed class OrderAuditRepository(InnoStoreContext context) : IOrderAuditRepository
{
    public async Task<OrderAudit> CreateAsync(OrderAudit orderAudit, CancellationToken cancellationToken = default)
    {
        var result = await context.OrderAudits.AddAsync(orderAudit, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return result.Entity;
    }

    public async Task CreateRangeAsync(IEnumerable<OrderAudit> audits, CancellationToken cancellationToken = default)
    {
        await context.OrderAudits.AddRangeAsync(audits.ToArray(), cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }
}