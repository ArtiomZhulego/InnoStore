using Application.Abstractions.DTOs.Entities;
using Domain.Entities;

namespace Application.Mappers;

internal static class OrderMapper
{
    extension(Order order)
    {
        public OrderDto ToDto()
        {
            return new OrderDto()
            {
                Id = order.Id,
                UserId = order.UserId,
                Status = order.Status,
            };
        }
    }
}