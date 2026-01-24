using Domain.Abstractions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

internal sealed class OrderRepository(InnoStoreContext context) : IOrderRepository
{
    public async Task<Order> CreateAsync(Order order, CancellationToken cancellationToken = default)
    {
        var result = await context.Orders.AddAsync(order, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return result.Entity;
    }

    public async Task<Order> UpdateAsync(Order order, CancellationToken cancellationToken = default)
    {
        order.UpdatedAt = DateTime.UtcNow;
        context.Orders.Update(order);
        await context.SaveChangesAsync(cancellationToken);
        return order;
    }

    public async Task<Order?> GetByIdAsync(Guid orderId, CancellationToken cancellationToken = default)
    {
        var result =
            await context.Orders.FirstOrDefaultAsync(order => order.Id.Equals(orderId), cancellationToken);
        return result;
    }

    public async Task<IReadOnlyList<Order>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        var result =
            await context.Orders.Where(order => order.UserId.Equals(userId)).ToListAsync(cancellationToken);
        return result;
    }
}