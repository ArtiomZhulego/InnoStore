using Domain.Abstractions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

internal sealed class OrderTransactionRepository(InnoStoreContext context) : IOrderTransactionsRepository
{
    public async Task AddAsync(OrderTransaction transaction, CancellationToken cancellationToken = default)
    {
        context.OrderTransactions.Add(transaction);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task<IReadOnlyCollection<OrderTransaction>> GetByOrderId(Guid orderId, CancellationToken cancellationToken = default) =>
        await context.OrderTransactions
            .AsNoTracking()
            .Where(item => item.OrderId == orderId)
            .Include(item => item.Transaction)
            .ToListAsync(cancellationToken);
}