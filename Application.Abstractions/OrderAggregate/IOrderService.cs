using Application.Abstractions.DTOs.Entities;
using Application.Abstractions.OrderAggregate.Models;

namespace Application.Abstractions.OrderAggregate;

public interface IOrderService
{
    public Task<OrderDto> CreateOrderAsync(CreateOrderModel model, CancellationToken cancellationToken = default);
    public Task<OrderDto?> CancelOrderAsync(CancelOrderModel cancelOrderModel, CancellationToken cancellationToken = default);
    public Task<OrderDto?> GetOrderByIdAsync(Guid orderId, CancellationToken cancellationToken = default);
    public Task<IEnumerable<OrderDto>> GetOrdersByUserAsync(Guid userId, CancellationToken cancellationToken = default);
}