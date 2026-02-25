using Domain.Entities;

namespace Domain.Abstractions;

public interface IOrderProductQuantityTransactionRepository
{
    public Task AddAsync(OrderProductQuantityTransaction orderProductQuantityTransaction, CancellationToken cancellationToken = default);
    public Task<IEnumerable<OrderProductQuantityTransaction>> GetByOrderIdAsync(Guid orderId, CancellationToken cancellationToken = default);
}