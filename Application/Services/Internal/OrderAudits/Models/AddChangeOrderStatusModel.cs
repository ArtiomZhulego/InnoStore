using Domain.ValueModels;
using Shared.ValueModels;

namespace Application.Services.Internal.OrderAudits.Models;

internal sealed class AddChangeOrderStatusModel
{
    public required Guid OrderId { get; init; }
    public required Guid UserId { get; init; }
    public required OrderStatus OrderStatus { get; init; }
}