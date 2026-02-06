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
            return new OrderDto()
            {
                Id = order.Id,
                UserId = order.UserId,
                Status = (OrderModelStatus)order.Status,
            };
        }
    }
}