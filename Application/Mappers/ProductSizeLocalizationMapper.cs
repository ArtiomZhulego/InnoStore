using Application.Abstractions.ProductSizeAggregate;
using Domain.Entities;

namespace Application.Mappers;

public static class ProductSizeLocalizationMapper
{
    extension(ProductSizeLocalization productSizeLocalization)
    {
        public ProductSizeLocalizationModel ToDTO()
        {
            return new ProductSizeLocalizationModel
            {
                Name = productSizeLocalization.Name,
                LanguageISOCode = productSizeLocalization.LanguageISOCode
            };
        }
    }

    extension(ProductSizeLocalizationModel productSizeLocalization)
    {
        public ProductSizeLocalization ToEntity(Guid productSizeId)
        {
            return new ProductSizeLocalization
            {
                Id = Guid.NewGuid(),
                ProductSizeId = productSizeId,
                Name = productSizeLocalization.Name,
                LanguageISOCode = productSizeLocalization.LanguageISOCode
            };
        }
    }
}
