namespace Domain.Entities;

public sealed class OrderTransaction : BaseEntity
{
    public required Guid OrderId { get; init; }
    public required Guid TransactionId { get; init; }
    
    public Order? Order { get; set; }
    public Transaction? Transaction { get; set; }
}