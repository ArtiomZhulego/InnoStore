using System.Linq;
using Application.Abstractions.ProductAggregate;
using Domain.Entities;

namespace Application.Mappers;

public static class ProductMapper
{
    public static ProductDTO ToDTO(this Product product)
    {
        var localization = product.Localizations.First();
        return new ProductDTO
        {
            Id = product.Id,
            Name = localization.Name,
            Description = localization.Description,
            Price = product.Price,
            ProductGroupId = product.ProductGroupId,
            ProductGroup = product.ProductGroup?.ToInformation(),
            Images = product.Images.Select(image => image.ToDTO()),
        };
    }

    public static ProductInformation ToInformation(this Product product)
    {
        var localization = product.Localizations.First();
        return new ProductInformation
        {
            Id = product.Id,
            Name = localization.Name,
            Description = localization.Description,
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
            Localizations = [.. model.Localizations.Select(loc => loc.ToEntity(id))]
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
