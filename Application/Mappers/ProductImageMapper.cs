using Application.Abstractions.ProductImageAggregate;
using Domain.Entities;

namespace Application.Mappers;

public static class ProductImageMapper
{
    extension(ProductImage productImage)
    {
        public ProductImageDTO ToDTO()
        {
            return new ProductImageDTO
            {
                Id = productImage.Id,
                ImageUrl = productImage.ImageUrl,
                ProductColorId = productImage.ProductColorId
            };
        }
    }

    extension(CreateProductImageModel createProductImage)
    {
        public ProductImage ToEntity(Guid productColorId)
        {
            return new ProductImage
            {
                ImageUrl = createProductImage.ImageUrl,
                ProductColorId = productColorId,
                OrderNumber = createProductImage.OrderNumber
            };
        }
    }

    extension(UpdateProductImageModel updateProductImage)
    {
        public ProductImage UpdateEntity(ProductImage productImage)
        {
            productImage.ImageUrl = updateProductImage.ImageUrl;
            productImage.OrderNumber = updateProductImage.OrderNumber;
            return productImage;
        }
    }
}
