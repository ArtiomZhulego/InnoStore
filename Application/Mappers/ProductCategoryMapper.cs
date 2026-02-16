using Application.Abstractions.ProductCategoryAggregate;
using Application.Constants;
using Domain.Entities;

namespace Application.Mappers;

public static class ProductCategoryMapper
{
    extension(ProductCategory productCategory)
    {
        public ProductCategoryDTO ToDTO()
        {
            return new ProductCategoryDTO
            {
                Id = productCategory.Id,
                Name = productCategory.Localizations.FirstOrDefault()?.Name ?? LocalizationConstants.DefaultTranslation,
                Products = productCategory.Products.Select(x => x.ToInformation())
            };
        }

        public ProductCategoryInformation ToInformation()
        {
            return new ProductCategoryInformation
            {
                Id = productCategory.Id,
                Name = productCategory.Localizations.FirstOrDefault()?.Name ?? LocalizationConstants.DefaultTranslation
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
        public ProductCategory UpdateEntity(ProductCategory productCategory)
        {
            foreach (var localizationModel in model.Localizations)
            {
                var localization = productCategory.Localizations
                    .FirstOrDefault(x => x.LanguageISOCode == localizationModel.LanguageISOCode);

                if (localization is not null)
                {
                    localization.Name = localizationModel.Name;
                }
                else
                {
                    productCategory.Localizations.Add(localizationModel.ToEntity(productCategory.Id));
                }
            }

            return productCategory;
        }
    }
}
