using Application.Abstractions.DTOs.Entities;
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
                ActionType = orderAudit.ActionType,
                Data = orderAudit.Data
            };
        }
    }
}