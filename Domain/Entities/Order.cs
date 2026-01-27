using Domain.ValueModels;

namespace Domain.Entities;

public sealed class Order : BaseEntity
{
    public required Guid Id { get; init; }
    public required Guid UserId { get; init; }
    public required OrderStatus Status { get; set; } = OrderStatus.Created;
    public required decimal Price { get; init; }

    public User? User { get; set; }
    public IEnumerable<OrderTransaction> OrderTransactions { get; set; } = [];
    public IEnumerable<OrderAudit> Audits { get; set; } = [];
}