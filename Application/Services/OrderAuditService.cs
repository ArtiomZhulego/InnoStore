using Application.Abstractions.DTOs.Entities;
using Application.Abstractions.OrderAggregate;
using Application.Mappers;
using Domain.Abstractions;

namespace Application.Services;

internal sealed class OrderAuditService(IOrderAuditRepository orderAuditRepository)
    : IOrderAuditService
{
    public async Task<IEnumerable<OrderAuditDto>> GetAuditByOfferAsync(Guid orderId, CancellationToken cancellationToken = default)
    {
        var items = await orderAuditRepository.GetOrderAuditsByOrderIdAsync(orderId, cancellationToken);
        return items.Select(item => item.ToDto());
    }
}