using Domain.Entities;

namespace Domain.Abstractions;

public interface IOrderRepository
{
    public Task CreateAsync(Order order, CancellationToken cancellationToken = default);
    public Task UpdateAsync(Order order, CancellationToken cancellationToken = default);
    public Task<Order?> GetByIdAsync(Guid orderId, CancellationToken cancellationToken = default);
    public Task<IEnumerable<Order>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
}