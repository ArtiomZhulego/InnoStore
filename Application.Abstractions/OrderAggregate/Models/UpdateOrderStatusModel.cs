using Application.Abstractions.DTOs.ValueModels;

namespace Application.Abstractions.OrderAggregate.Models;

public sealed class UpdateOrderStatusModel
{
    public required Guid OrderId { get; set; }
    public required OrderModelStatus OrderModelStatus { get; set; }
}