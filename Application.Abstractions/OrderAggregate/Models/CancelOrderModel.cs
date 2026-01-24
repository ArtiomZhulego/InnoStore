namespace Application.Abstractions.OrderAggregate.Models;

public sealed class CancelOrderModel
{
    public required Guid OrderId { get; init; }
    public required Guid RevertedByUserId { get; init; }
}