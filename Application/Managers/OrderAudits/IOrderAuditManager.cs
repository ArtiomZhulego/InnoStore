using Application.Managers.OrderAudits.Models;
using Domain.Entities;

namespace Application.Managers.OrderAudits;

internal interface IOrderAuditManager
{
    public Task<OrderAudit> AddChangeOrderStatusAsync(AddChangeOrderStatusModel model, CancellationToken cancellationToken = default);
}