using System.Linq;
using Application.Abstractions.ProductAggregate;
using Application.Constants;
using Domain.Entities;

namespace Application.Mappers;

public static class ProductMapper
{
    extension(Product product)
    {
        public ProductDTO ToDTO()
        {
            var localization = product.Localizations.FirstOrDefault();
            return new ProductDTO
            {
                Id = product.Id,
                Name = localization?.Name ?? LocalizationConstants.DefaultTranslation,
                Description = localization?.Description ?? LocalizationConstants.DefaultTranslation,
                Price = product.Price,
                ProductGroupId = product.ProductGroupId,
                ProductGroup = product.ProductGroup?.ToInformation(),
                Images = product.Images.Select(image => image.ToDTO()),
                Sizes = product.Sizes.Select(size => size.ToDTO())
            };
        }

        public ProductInformation ToInformation()
        {
            var localization = product.Localizations.FirstOrDefault();
            return new ProductInformation
            {
                Id = product.Id,
                Name = localization?.Name ?? LocalizationConstants.DefaultTranslation,
                Description = localization?.Description ?? LocalizationConstants.DefaultTranslation,
                Price = product.Price,
                ImageUrl = product.Images.FirstOrDefault()?.ImageUrl ?? string.Empty
            };
        }
    }

    extension(CreateProductModel model)
    {
        public Product ToEntity()
        {
            var id = Guid.NewGuid();
            return new Product
            {
                Id = id,
                Price = model.Price,
                ProductGroupId = model.ProductGroupId,
                Localizations = [.. model.Localizations.Select(loc => loc.ToEntity(id))],
                Sizes = [.. model.Sizes.Select(size => size.ToEntity(id))],
                Images = [.. model.Images.Select(image => image.ToEntity(id))],
            };
        }
    }

    extension(UpdateProductModel model)
    {
        public Product UpdateEntity(Product product)
        {
            product.Price = model.Price;
            product.ProductGroupId = model.ProductGroupId;

            foreach (var localizationModel in model.Localizations)
            {
                var localization = product.Localizations
                    .FirstOrDefault(x => x.LanguageISOCode == localizationModel.LanguageISOCode);

                if (localization != null)
                {
                    localization.Name = localizationModel.Name;
                }
                else
                {
                    product.Localizations.Add(localizationModel.ToEntity(product.Id));
                }
            }

            return product;
        }
    }
}
