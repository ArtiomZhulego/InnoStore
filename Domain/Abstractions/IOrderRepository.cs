using Domain.Entities;

namespace Domain.Abstractions;

public interface IOrderRepository
{
    public Task<Order> CreateAsync(Order order, CancellationToken cancellationToken = default);
    public Task<Order> UpdateAsync(Order order, CancellationToken cancellationToken = default);
    public Task<Order?> GetByIdAsync(Guid orderId, CancellationToken cancellationToken = default);
    public Task<IReadOnlyList<Order>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
}