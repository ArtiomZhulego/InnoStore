using Domain.ValueModels;

namespace Domain.Entities;

public sealed class ProductQuantityTransaction : BaseEntity
{
    public required Guid Id { get; init; }
    public required ProductQuantityTransactionType EventType { get; init; }
    public required int OperationAmount { get; init; }
    public required Guid UserId { get; init; }
    public required Guid ProductSizeId { get; init; }
    public Guid? OrderProductQuantityTransactionId {get; init; }

    public User? User { get; set; }
    public ProductSize? ProductSize { get; set; }
    public OrderProductQuantityTransaction? OrderProductQuantityTransaction { get; set; }
}
