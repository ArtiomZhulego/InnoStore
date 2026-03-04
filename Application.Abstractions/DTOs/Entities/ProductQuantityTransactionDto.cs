using Shared.ValueModels;

namespace Application.Abstractions.DTOs.Entities;

public sealed record ProductQuantityTransactionDto
{
    public required Guid Id { get; init; }
    public required ProductQuantityTransactionType EventType { get; init; }
    public required int OperationAmount { get; init; }
    public required Guid UserId { get; init; }
    public required Guid ProductSizeId { get; init; }
    public required DateTime CreatedAt { get; init; }
}
