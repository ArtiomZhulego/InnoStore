using Domain.Entities;

namespace Domain.Abstractions;

public interface IOrderAuditRepository
{
    public Task CreateAsync(OrderAudit orderAudit, CancellationToken cancellationToken = default);
    public Task CreateRangeAsync(IEnumerable<OrderAudit> audits, CancellationToken cancellationToken = default);
    public Task<IEnumerable<OrderAudit>> GetOrderAuditsByOrderIdAsync(Guid orderId, CancellationToken cancellationToken = default);
}