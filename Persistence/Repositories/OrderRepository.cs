using Domain.Abstractions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

internal sealed class OrderRepository(InnoStoreContext context) : IOrderRepository
{
    public async Task CreateAsync(Order order, CancellationToken cancellationToken = default)
    {
        await context.Orders.AddAsync(order, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Order order, CancellationToken cancellationToken = default)
    {
        context.Orders.Update(order);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task<Order?> GetByIdAsync(Guid orderId, CancellationToken cancellationToken = default)
    {
        var result =
            await context.Orders.FirstOrDefaultAsync(order => order.Id == orderId, cancellationToken);
        return result;
    }

    public async Task<IEnumerable<Order>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        var result = await context.Orders
            .AsNoTracking()
            .Where(order => order.UserId == userId)
            .ToListAsync(cancellationToken);
        return result;
    }
}