using Domain.Abstractions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

public sealed class OrderTransactionRepository(InnoStoreContext context) : IOrderTransactionsRepository
{
    public async Task AddAsync(OrderTransaction transaction, CancellationToken cancellationToken = default)
    {
        await context.OrderTransactions.AddAsync(transaction, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task<IReadOnlyCollection<OrderTransaction>> GetByOrderId(Guid orderId, CancellationToken cancellationToken = default)
    {
        var result = await context.OrderTransactions
            .AsNoTracking()
            .Where(item => item.OrderId == orderId)
            .ToListAsync(cancellationToken);
        return result;
    }
}