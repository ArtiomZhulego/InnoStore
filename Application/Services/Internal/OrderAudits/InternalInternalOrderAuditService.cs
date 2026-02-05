using System.Text.Json;
using Application.Services.Internal.OrderAudits.Models;
using Domain.Abstractions;
using Domain.Entities;
using Domain.ValueModels;

namespace Application.Services.Internal.OrderAudits;

internal sealed class InternalInternalOrderAuditService(IOrderAuditRepository orderAuditRepository) : IInternalOrderAuditService
{
    public async Task<OrderAudit> AddChangeOrderStatusAsync(AddChangeOrderStatusModel model, CancellationToken cancellationToken = default)
    {
        var changesPayload = new
        {
            NewStatus = model.OrderStatus.ToString(),
            ChangedAt = DateTime.UtcNow
        };

        var audit = new OrderAudit
        {
            Id = Guid.NewGuid(),
            OrderId = model.OrderId,
            ChangedByUserId = model.UserId,
            ActionType = OrderActionType.StatusChanged,
            Data = JsonSerializer.Serialize(changesPayload),
            CorrelationId = Guid.NewGuid()
        };

        await orderAuditRepository.CreateAsync(audit, cancellationToken);
        return audit;
    }
}