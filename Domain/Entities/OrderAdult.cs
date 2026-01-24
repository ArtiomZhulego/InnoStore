using Domain.ValueModels;

namespace Domain.Entities;

public sealed class OrderAudit : BaseEntity
{
    public long Id { get; set; }
    public Guid CorrelationId { get; set; }
    public Guid OrderId { get; set; }
    public Guid ChangedByUserId { get; set; }
    public required OrderActionType ActionType { get; set; }
    public required string Data { get; set; }
    
    public Order? Order { get; set; }
}