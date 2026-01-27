namespace Application.Abstractions.OrderAggregate.Models;

public sealed record CancelOrderModel
{
    public required Guid OrderId { get; init; }
    public required Guid RevertedByUserId { get; init; }
}