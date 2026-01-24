using Domain.Entities;

namespace Domain.Abstractions;

public interface IOrderAuditRepository
{
    public Task<OrderAudit> CreateAsync(OrderAudit orderAudit, CancellationToken cancellationToken = default);
    public Task CreateRangeAsync(IEnumerable<OrderAudit> audits, CancellationToken cancellationToken = default);
}