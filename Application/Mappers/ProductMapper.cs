using System.Linq;
using Application.Abstractions.ProductAggregate;
using Domain.Entities;
using Shared.Constants;

namespace Application.Mappers;

public static class ProductMapper
{
    public static ProductDTO ToDTO(this Product product)
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

    public static ProductInformation ToInformation(this Product product)
    {
        var localization = product.Localizations.FirstOrDefault();
        return new ProductInformation
        {
            Id = product.Id,
            Name = localization?.Name ?? LocalizationConstants.DefaultTranslation,
            Description = localization?.Description ?? LocalizationConstants.DefaultTranslation,
            Price = product.Price
        };
    }

    public static Product ToEntity(this CreateProductModel model)
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

    public static Product UpdateEntity(this UpdateProductModel model, Product product)
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
