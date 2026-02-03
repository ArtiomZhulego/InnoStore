using Application.Abstractions.DTOs.Entities;

namespace Application.Abstractions.OrderAggregate;

public interface IOrderAuditService
{
    public Task<IEnumerable<OrderAuditDto>> GetAuditByOrderAsync(Guid orderId, CancellationToken cancellationToken = default);
}