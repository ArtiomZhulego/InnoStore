using Application.Abstractions.ProductGroupAggregate;
using Domain.Entities;

namespace Application.Mappers;

public static class ProductGroupLocalizationMapper
{
    extension(ProductCategoryLocalizationModel localization)
    {
        public ProductCategoryLocalization ToEntity(Guid productGroupId)
        {
            return new ProductCategoryLocalization
            {
                Id = Guid.NewGuid(),
                ProductGroupId = productGroupId,
                LanguageISOCode = localization.LanguageISOCode,
                Name = localization.Name,
                Description = localization.Description
            };
        }
    }
}
