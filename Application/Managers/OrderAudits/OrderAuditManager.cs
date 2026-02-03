using Application.Managers.OrderAudits.Models;
using Domain.Abstractions;
using Domain.Entities;
using Domain.ValueModels;
using System.Text.Json;

namespace Application.Managers.OrderAudits;

internal sealed class OrderAuditManager(IOrderAuditRepository orderAuditRepository) : IOrderAuditManager
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