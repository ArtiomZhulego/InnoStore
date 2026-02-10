using Application.Services.Internal.OrderAudits.Models;
using Domain.Entities;

namespace Application.Services.Internal.OrderAudits;

internal interface IInternalOrderAuditService
{
    public Task<OrderAudit> AddChangeOrderStatusAsync(Guid userId, Order order, CancellationToken cancellationToken = default);
}