using Domain.Abstractions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

public sealed class OrderTransactionRepository(InnoStoreContext context) : IOrderTransactionsRepository
{
    public async Task<OrderTransaction> AddAsync(OrderTransaction transaction, CancellationToken cancellationToken = default)
    {
        var result = await context.OrderTransactions.AddAsync(transaction, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return result.Entity;
    }

    public async Task<IEnumerable<OrderTransaction>> GetByOrderId(Guid orderId, CancellationToken cancellationToken = default)
    {
        var result = await context.OrderTransactions.Where(item => item.OrderId.Equals(orderId)).ToListAsync();
        return result;
    }
}