using Domain.Entities;

namespace Domain.Abstractions;

public interface IOrderTransactionsRepository
{
    public Task AddAsync(OrderTransaction transaction, CancellationToken cancellationToken = default);
    public Task<IEnumerable<OrderTransaction>> GetByOrderId(Guid orderId, CancellationToken cancellationToken = default);
}