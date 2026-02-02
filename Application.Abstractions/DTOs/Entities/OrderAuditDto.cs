using Application.Abstractions.DTOs.ValueModels;

namespace Application.Abstractions.DTOs.Entities;

public sealed class OrderAuditDto
{
    public Guid Id { get; set; }
    public Guid CorrelationId { get; set; }
    public Guid OrderId { get; set; }
    public Guid ChangedByUserId { get; set; }
    public required OrderActionType ActionType { get; set; }
    public required string Data { get; set; }
}