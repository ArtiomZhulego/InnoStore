namespace Application.Abstractions.OrderAggregate.Models;

public sealed class CreateOrderModel
{
    public required Guid UserId { get; init; }
    public required Guid ProductSizeId { get; init; }
}