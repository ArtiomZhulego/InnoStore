using Application.Abstractions.DTOs.Entities;
using Application.Abstractions.DTOs.ValueModels;
using Domain.Entities;
using Domain.ValueModels;

namespace Application.Mappers;

internal static class OrderMapper
{
    extension(Order order)
    {
        public OrderDto ToDto()
        {
            if (order is null) throw new ArgumentNullException(nameof(order));
            
            return new OrderDto()
            {
                Id = order.Id,
                UserId = order.UserId,
                Status = (OrderModelStatus)order.Status,
            };
        }
    }
}