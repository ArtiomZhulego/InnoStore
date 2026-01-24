using Application.Abstractions.DTOs.Entities;

namespace Application.Abstractions.OrderAggregate;

public interface IOrderAuditService
{
    public Task<IEnumerable<OrderAuditDto>> GetAuditByOfferAsync(Guid offerId, CancellationToken cancellationToken = default);
}