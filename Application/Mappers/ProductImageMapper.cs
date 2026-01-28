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

    public static ProductImage ToEntity(this CreateProductImageModel createProductImage, Guid productId)
    {
        return new ProductImage
        {
            ImageUrl = createProductImage.ImageUrl,
            ProductId = productId
        };
    }

    public static ProductImage UpdateEntity(this UpdateProductImageModel updateProductImage, ProductImage productImage)
    {
        productImage.ImageUrl = updateProductImage.ImageUrl;
        return productImage;
    }
}
