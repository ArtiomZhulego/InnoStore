using Application.Abstractions.DTOs.ValueModels;

namespace Application.Abstractions.DTOs.Entities;

public sealed class OrderDto
{
    public required Guid Id { get; init; }
    public required Guid UserId { get; init; }
    public required OrderModelStatus Status { get; init; }
}