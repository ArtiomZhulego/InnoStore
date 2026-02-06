namespace Application.Abstractions.OrderAggregate.Models;

public sealed record CreateOrderModel
{
    public required Guid UserId { get; init; }
    public required Guid ProductSizeId { get; init; }
}