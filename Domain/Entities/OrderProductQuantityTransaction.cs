namespace Domain.Entities;

public sealed class OrderProductQuantityTransaction : BaseEntity
{
    public required Guid Id { get; init; }
    public required Guid OrderId { get; init; }
    public required Guid ProductQuantityTransactionId { get; init; }

    public Order? Order { get; set; }
    public ProductQuantityTransaction? ProductQuantityTransaction { get; set; }
}
