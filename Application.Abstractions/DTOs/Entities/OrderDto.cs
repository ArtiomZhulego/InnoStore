using Shared.ValueModels;

namespace Application.Abstractions.DTOs.Entities;

public sealed record OrderDto
{
    public required Guid Id { get; init; }
    public required Guid UserId { get; init; }
    public required OrderStatus Status { get; init; }
}