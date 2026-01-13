using Application.Abstractions.ProductAggregate;
using Domain.Entities;

namespace Application.Mappers;

public static class ProductMapper
{
    public static ProductDTO ToDTO(this Product product)
    {
        return new ProductDTO
        {
            Id = product.Id,
            Name = product.Localizations.First().Name,
            Price = product.Price,
            ProductGroupId = product.ProductGroupId,
            ProductGroup = product.ProductGroup?.ToDTO(),
            Images = product.Images.Select(image => image.ToDTO()),
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

        foreach (var localization in product.Localizations)
        {
            var updatedLocalization = model.Localizations
                .FirstOrDefault(loc => loc.LanguageISOCode == localization.LanguageISOCode);
            if (updatedLocalization is null)
            {
                continue;
            }

            localization.Name = updatedLocalization.Name;
        }

        return product;
    }
}
