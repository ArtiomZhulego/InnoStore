using Application.Abstractions.ProductCategoryAggregate;
using Domain.Entities;

namespace Application.Mappers;

public static class ProductCategoryLocalizationMapper
{
    extension(ProductCategoryLocalizationModel localization)
    {
        public ProductCategoryLocalization ToEntity(Guid productCategoryId)
        {
            return new ProductCategoryLocalization
            {
                Id = Guid.NewGuid(),
                ProductCategoryId = productCategoryId,
                LanguageISOCode = localization.LanguageISOCode,
                Name = localization.Name,
                Description = localization.Description
            };
        }
    }

    extension(ProductCategoryLocalization localization)
    {
        public ProductCategoryLocalizationModel ToModel()
        {
            return new ProductCategoryLocalizationModel
            {
                LanguageISOCode = localization.LanguageISOCode,
                Name = localization.Name,
                Description = localization.Description
            };
        }
    }
}
