using Domain.Abstractions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

internal sealed class OrderAuditRepository(InnoStoreContext context) : IOrderAuditRepository
{
    public async Task CreateAsync(OrderAudit orderAudit, CancellationToken cancellationToken = default)
    {
        await context.OrderAudits.AddAsync(orderAudit, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task CreateRangeAsync(IEnumerable<OrderAudit> audits, CancellationToken cancellationToken = default)
    {
        await context.OrderAudits.AddRangeAsync(audits, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task<IEnumerable<OrderAudit>> GetOrderAuditsByOrderIdAsync(Guid orderId, CancellationToken cancellationToken = default)
    {
        var result = await context.OrderAudits
            .Where(item => item.OrderId == orderId)
            .OrderByDescending(item => item.CreatedAt)
            .ToListAsync(cancellationToken);
        return result;
    }
}