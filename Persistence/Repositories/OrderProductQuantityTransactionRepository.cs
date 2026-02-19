using Domain.Abstractions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

internal sealed class OrderProductQuantityTransactionRepository(InnoStoreContext context) : IOrderProductQuantityTransactionRepository
{
    public async Task AddAsync(OrderProductQuantityTransaction orderProductQuantityTransaction, CancellationToken cancellationToken = default)
    {
        context.OrderProductQuantityTransactions.Add(orderProductQuantityTransaction);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task<OrderProductQuantityTransaction?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
        await context.OrderProductQuantityTransactions.FirstOrDefaultAsync(item => item.Id == id, cancellationToken);

    public async Task<IEnumerable<OrderProductQuantityTransaction>> GetByOrderIdAsync(Guid orderId, CancellationToken cancellationToken = default) =>
        await context.OrderProductQuantityTransactions.Where(item => item.OrderId == orderId).ToListAsync(cancellationToken);
}
