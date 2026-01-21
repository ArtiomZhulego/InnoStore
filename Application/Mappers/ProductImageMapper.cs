using Application.Abstractions.ProductImageAggregate;
using Domain.Entities;

namespace Application.Mappers;

public static class ProductImageMapper
{
    public static ProductImageDTO ToDTO(this ProductImage productImage)
    {
        return new ProductImageDTO
        {
            Id = productImage.Id,
            ImageUrl = productImage.ImageUrl,
            ProductId = productImage.ProductId
        };
    }
}
