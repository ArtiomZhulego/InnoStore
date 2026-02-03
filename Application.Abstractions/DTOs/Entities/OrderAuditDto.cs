using Application.Abstractions.DTOs.ValueModels;

namespace Application.Abstractions.DTOs.Entities;

public sealed class OrderAuditDto
{
    public required Guid Id { get; init; }
    public required Guid CorrelationId { get; init; }
    public required Guid OrderId { get; init; }
    public required Guid ChangedByUserId { get; init; }
    public required OrderActionType ActionType { get; init; }
    public required string Data { get; init; }
}