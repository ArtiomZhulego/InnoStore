using Application.Abstractions.DTOs.ValueModels;

namespace Application.Abstractions.OrderAggregate.Models;

public sealed record UpdateOrderStatusModel
{
    public required Guid OrderId { get; init; }
    public required OrderModelStatus OrderModelStatus { get; init; }
}