using Application.Abstractions.ProductAggregate;
using Application.Extensions;
using Application.Mappers;
using Domain.Abstractions;
using Domain.Entities;
using Domain.Exceptions;

namespace Application.Services;

public class ProductService(IProductRepository productRepository,
                            IProductGroupRepository productGroupRepository,
                            IStorageService storageService) : IProductService
{
    public async Task<ProductDTO> CreateAsync(CreateProductModel createProductModel, CancellationToken cancellationToken = default)
    {
        var productGroupExist = await productGroupRepository.ExistAsync(createProductModel.ProductGroupId, cancellationToken);

        if (!productGroupExist)
        {
            throw new ProductGroupNotFoundException(createProductModel.ProductGroupId);
        }

        foreach(var image in createProductModel.Images)
        {
            var imageExists = await storageService.ExistsAsync(image.ImageUrl, cancellationToken);
            if (imageExists)
            {
                continue;
            }

            throw new ImageNotFoundException(image.ImageUrl);
        }

        var product = createProductModel.ToEntity();
        await productRepository.CreateAsync(product, cancellationToken);

        await FillImageUrlsAsync(product.Images, cancellationToken);

        return product.ToDTO();
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var product = await productRepository.GetByIdAsync(id, cancellationToken) ?? throw new ProductNotFoundException(id);

        await productRepository.DeleteAsync(product, cancellationToken);
    }

    public async Task<IEnumerable<ProductDTO>> GetByGroupAsync(Guid groupId, string languageCode, CancellationToken cancellationToken = default)
    {
        var productGroupExist = await productGroupRepository.ExistAsync(groupId, cancellationToken);
        if (!productGroupExist)
        {
            throw new ProductGroupNotFoundException(groupId);
        }

        var products = await productRepository.GetByGroupIdAsync(groupId, languageCode, cancellationToken) ?? throw new ProductNotFoundException(groupId);

        var images = products.SelectMany(p => p.Images);
        await FillImageUrlsAsync(images, cancellationToken);

        return products.Select(x => x.ToDTO());
    }

    public async Task<ProductDTO> GetByIdAsync(Guid id, string languageCode, CancellationToken cancellationToken = default)
    {
        var product = await productRepository.GetByIdAsync(id, languageCode, cancellationToken) ?? throw new ProductNotFoundException(id);

        await FillImageUrlsAsync(product.Images, cancellationToken);
        return product.ToDTO();
    }

    public async Task<ProductDTO> UpdateAsync(Guid id, UpdateProductModel updateProductModel, CancellationToken cancellationToken = default)
    {
        var productGroupExist = await productGroupRepository.ExistAsync(updateProductModel.ProductGroupId, cancellationToken);
        if (!productGroupExist)
        {
            throw new ProductGroupNotFoundException(updateProductModel.ProductGroupId);
        }

        foreach (var image in updateProductModel.Images)
        {
            var imageExists = await storageService.ExistsAsync(image.ImageUrl, cancellationToken);
            if (imageExists)
            {
                continue;
            }

            throw new ImageNotFoundException(image.ImageUrl);
        }

        var product = await productRepository.GetByIdAsync(id, cancellationToken) ?? throw new ProductNotFoundException(id);

        product = updateProductModel.UpdateEntity(product);

        await productRepository.UpdateAsync(product, cancellationToken);
        return product.ToDTO();
    }

    private async Task FillImageUrlsAsync(IEnumerable<ProductImage> images, CancellationToken cancellationToken)
    {
        await Parallel.ForEachAsync(images, cancellationToken, async (image, ct) =>
        {
            image.ImageUrl = await storageService.GetQuickAccessUrlAsync(image.ImageUrl);
        });
    }
}
