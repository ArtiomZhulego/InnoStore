using Domain.Abstractions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

internal sealed class OrderAuditRepository(InnoStoreContext context) : IOrderAuditRepository
{
    public async Task CreateAsync(OrderAudit orderAudit, CancellationToken cancellationToken = default)
    {
        context.OrderAudits.Add(orderAudit);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task CreateRangeAsync(IEnumerable<OrderAudit> audits, CancellationToken cancellationToken = default)
    {
        context.OrderAudits.AddRange(audits);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task<IEnumerable<OrderAudit>> GetOrderAuditsByOrderIdAsync(Guid orderId, CancellationToken cancellationToken = default) =>
        await context.OrderAudits
            .Where(item => item.OrderId == orderId)
            .OrderByDescending(item => item.CreatedAt)
            .Include(item => item.Order)
            .ToListAsync(cancellationToken);
}