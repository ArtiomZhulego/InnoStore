using Application.Abstractions.ProductGroupAggregate;
using Application.Constants;
using Domain.Entities;

namespace Application.Mappers;

public static class ProductGroupMapper
{
    extension(ProductCategory productGroup)
    {
        public ProductCategoryDTO ToDTO()
        {
            return new ProductCategoryDTO
            {
                Id = productGroup.Id,
                Name = productGroup.Localizations.FirstOrDefault()?.Name ?? LocalizationConstants.DefaultTranslation,
                Products = productGroup.Products.Select(x => x.ToInformation())
            };
        }

        public ProductCategoryInformation ToInformation()
        {
            return new ProductCategoryInformation
            {
                Id = productGroup.Id,
                Name = productGroup.Localizations.FirstOrDefault()?.Name ?? LocalizationConstants.DefaultTranslation
            };
        }
    }

    extension(CreateProductCategoryModel model)
    {
        public ProductCategory ToEntity()
        {
            var id = Guid.NewGuid();
            return new ProductCategory
            {
                Id = id,
                Localizations = [.. model.Localizations.Select(x => x.ToEntity(id))]
            };
        }
    }

    extension(UpdateProductCategoryModel model)
    {
        public ProductCategory UpdateEntity(ProductCategory productGroup)
        {
            foreach (var localizationModel in model.Localizations)
            {
                var localization = productGroup.Localizations
                    .FirstOrDefault(x => x.LanguageISOCode == localizationModel.LanguageISOCode);

                if (localization is not null)
                {
                    localization.Name = localizationModel.Name;
                }
                else
                {
                    productGroup.Localizations.Add(localizationModel.ToEntity(productGroup.Id));
                }
            }

            return productGroup;
        }
    }
}
