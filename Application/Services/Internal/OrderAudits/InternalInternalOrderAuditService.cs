using System.Text.Json;
using Domain.Abstractions;
using Domain.Entities;
using Shared.ValueModels;

namespace Application.Services.Internal.OrderAudits;

internal sealed class InternalInternalOrderAuditService(IOrderAuditRepository orderAuditRepository) : IInternalOrderAuditService
{
    public async Task<OrderAudit> AddChangeOrderStatusAsync(Guid userId, Order order, CancellationToken cancellationToken = default)
    {
        var changesPayload = new
        {
            NewStatus = order.Status.ToString(),
            ChangedAt = DateTime.UtcNow
        };

        var audit = new OrderAudit
        {
            Id = Guid.NewGuid(),
            OrderId = order.Id,
            ChangedByUserId = userId,
            ActionType = OrderActionType.StatusChanged,
            Data = JsonSerializer.Serialize(changesPayload),
            CorrelationId = Guid.NewGuid()
        };

        await orderAuditRepository.CreateAsync(audit, cancellationToken);
        return audit;
    }
}