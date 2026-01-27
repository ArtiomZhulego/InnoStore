using Application.Managers.OrderAudits.Models;
using Domain.Abstractions;
using Domain.Entities;

namespace Application.Managers.OrderAudits;

internal sealed class OrderAuditManager(IOrderAuditRepository orderAuditRepository) : IOrderAuditManager
{
    public Task<OrderAudit?> AddChangeOrderStatusAsync(AddChangeOrderStatusModel model, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}