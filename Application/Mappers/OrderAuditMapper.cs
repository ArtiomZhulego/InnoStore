using Application.Abstractions.DTOs.Entities;
using Application.Abstractions.DTOs.ValueModels;
using Domain.Entities;

namespace Application.Mappers;

internal static class OrderAuditMapper
{
    extension(OrderAudit orderAudit)
    {
        public OrderAuditDto ToDto()
        {
            return new OrderAuditDto
            {
                Id = orderAudit.Id,
                CorrelationId = orderAudit.CorrelationId,
                OrderId = orderAudit.OrderId,
                ChangedByUserId = orderAudit.ChangedByUserId,
                ActionType = (OrderActionType)orderAudit.ActionType,
                Data = orderAudit.Data
            };
        }
    }
}