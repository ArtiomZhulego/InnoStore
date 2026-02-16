using System.Linq;
using Application.Abstractions.ProductAggregate;
using Application.Abstractions.ProductBatchAggregate;
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
                ProductCategoryId = product.ProductCategoryId,
                ProductCategory = product.ProductCategory?.ToInformation(),
                Sizes = product.Sizes.Select(size => size.ToDTO()),
                Colors = product.Colors.Select(color => color.ToDTO())
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
                ImageUrl = product.Colors.FirstOrDefault()?.Images.FirstOrDefault()?.ImageUrl ?? string.Empty
            };
        }
    }

    extension(ProductModel productModel)
    {
        public CreateProductModel ToCreateModel(Guid productGroupId)
        {
            return new CreateProductModel
            {
                Price = productModel.Price,
                ProductGroupId = productGroupId,
                Localizations = productModel.Localizations,
                Sizes = productModel.Sizes,
                Colors = productModel.Colors
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
                ProductCategoryId = model.ProductGroupId,
                Localizations = [.. model.Localizations.Select(loc => loc.ToEntity(id))],
                Sizes = [.. model.Sizes.Select(size => size.ToEntity(id))],
                Colors = [.. model.Colors.Select(color => color.ToEntity(id))],
            };
        }
    }

    extension(UpdateProductModel model)
    {
        public Product UpdateEntity(Product product)
        {
            product.Price = model.Price;
            product.ProductCategoryId = model.ProductGroupId;

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
