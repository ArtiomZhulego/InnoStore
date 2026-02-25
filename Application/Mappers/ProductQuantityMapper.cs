using Application.Abstractions.DTOs.Entities;
using Domain.Entities;

namespace Application.Mapper;

internal static class ProductQuantityMapper
{
    extension (ProductQuantityTransaction entity)
    {
        public ProductQuantityTransactionDto ToDto()
        {
            return new ProductQuantityTransactionDto
            {
                Id = entity.Id,
                EventType = entity.EventType,
                OperationAmount = entity.OperationAmount,
                UserId = entity.UserId,
                ProductSizeId = entity.ProductSizeId,
                CreatedAt = entity.CreatedAt
            };
        }
    }

    public static IEnumerable<ProductQuantityTransactionDto> ToDto(this IEnumerable<ProductQuantityTransaction> entities)
    {
        return entities.Select(e => e.ToDto());
    }
}
