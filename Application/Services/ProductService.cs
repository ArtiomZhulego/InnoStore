using Application.Abstractions.ProductAggregate;
using Application.Abstractions.StorageAggregate;
using Application.Extensions;
using Application.Mappers;
using Domain.Abstractions;
using Domain.Entities;
using Domain.Exceptions;
using FluentValidation;

namespace Application.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IProductGroupRepository _productGroupRepository;
    private readonly IValidator<CreateProductModel> _createValidator;
    private readonly IValidator<UpdateProductModel> _updateValidator;
    private readonly IStorageService _storageService;

    public ProductService(IProductRepository productRepository, IValidator<CreateProductModel> createValidator, IProductGroupRepository productGroupRepository, IValidator<UpdateProductModel> updateValidator, IStorageService storageService)
    {
        _productRepository = productRepository;
        _createValidator = createValidator;
        _productGroupRepository = productGroupRepository;
        _updateValidator = updateValidator;
        _storageService = storageService;
    }

    public async Task<ProductDTO> CreateAsync(CreateProductModel createProductModel, CancellationToken cancellationToken = default)
    {
        await _createValidator.EnsureValidAsync(createProductModel, cancellationToken: cancellationToken);

        var productGroupExist = await _productGroupRepository.ExistAsync(createProductModel.ProductGroupId, cancellationToken);

        if (!productGroupExist)
        {
            throw new ProductGroupNotFoundException(createProductModel.ProductGroupId);
        }

        foreach(var image in createProductModel.Images)
        {
            var imageExists = await _storageService.ExistsAsync(image.ImageUrl, cancellationToken);
            if (imageExists)
            {
                continue;
            }

            throw new ImageNotFoundException(image.ImageUrl);
        }

        var product = createProductModel.ToEntity();
        await _productRepository.CreateAsync(product, cancellationToken);

        await FillImageUrlsAsync(product.Images, cancellationToken);

        return product.ToDTO();
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var product = await _productRepository.GetByIdAsync(id, cancellationToken) ?? throw new ProductNotFoundException(id);

        await _productRepository.DeleteAsync(product, cancellationToken);
    }

    public async Task<IEnumerable<ProductDTO>> GetByGroupAsync(Guid groupId, string languageCode, CancellationToken cancellationToken = default)
    {
        var productGroupExist = await _productGroupRepository.ExistAsync(groupId, cancellationToken);
        if (!productGroupExist)
        {
            throw new ProductGroupNotFoundException(groupId);
        }

        var products = await _productRepository.GetByGroupIdAsync(groupId, languageCode, cancellationToken) ?? throw new ProductNotFoundException(groupId);

        var images = products.SelectMany(p => p.Images);
        await FillImageUrlsAsync(images, cancellationToken);

        return products.Select(x => x.ToDTO());
    }

    public async Task<ProductDTO> GetByIdAsync(Guid id, string languageCode, CancellationToken cancellationToken = default)
    {
        var product = await _productRepository.GetByIdAsync(id, languageCode, cancellationToken) ?? throw new ProductNotFoundException(id);

        await FillImageUrlsAsync(product.Images, cancellationToken);
        return product.ToDTO();
    }

    public async Task<ProductDTO> UpdateAsync(Guid id, UpdateProductModel updateProductModel, CancellationToken cancellationToken = default)
    {
        await _updateValidator.EnsureValidAsync(updateProductModel, cancellationToken: cancellationToken);

        var productGroupExist = await _productGroupRepository.ExistAsync(updateProductModel.ProductGroupId, cancellationToken);
        if (!productGroupExist)
        {
            throw new ProductGroupNotFoundException(updateProductModel.ProductGroupId);
        }

        foreach (var image in updateProductModel.Images)
        {
            var imageExists = await _storageService.ExistsAsync(image.ImageUrl, cancellationToken);
            if (imageExists)
            {
                continue;
            }

            throw new ImageNotFoundException(image.ImageUrl);
        }

        var product = await _productRepository.GetByIdAsync(id, cancellationToken) ?? throw new ProductNotFoundException(id);

        product = updateProductModel.UpdateEntity(product);

        await _productRepository.UpdateAsync(product, cancellationToken);
        return product.ToDTO();
    }

    private async Task FillImageUrlsAsync(IEnumerable<ProductImage> images, CancellationToken cancellationToken)
    {
        await Parallel.ForEachAsync(images, cancellationToken, async (image, ct) =>
        {
            image.ImageUrl = await _storageService.GetQuickAccessUrlAsync(image.ImageUrl);
        });
    }
}
