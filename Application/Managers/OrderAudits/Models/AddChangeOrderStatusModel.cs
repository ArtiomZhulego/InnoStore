using Domain.ValueModels;

namespace Application.Managers.OrderAudits.Models;

internal sealed class AddChangeOrderStatusModel
{
    public required Guid OrderId { get; init; }
    public required Guid UserId { get; init; }
    public required OrderStatus OrderStatus { get; init; }
}