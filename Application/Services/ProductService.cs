using Application.Abstractions.ProductAggregate;
using Application.Extensions;
using Application.Mappers;
using Domain.Abstractions;
using Domain.Exceptions;
using FluentValidation;

namespace Application.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IProductGroupRepository _productGroupRepository;
    private readonly IValidator<CreateProductModel> _createValidator;
    private readonly IValidator<UpdateProductModel> _updateValidator;

    public ProductService(IProductRepository productRepository, IValidator<CreateProductModel> createValidator, IProductGroupRepository productGroupRepository, IValidator<UpdateProductModel> updateValidator)
    {
        _productRepository = productRepository;
        _createValidator = createValidator;
        _productGroupRepository = productGroupRepository;
        _updateValidator = updateValidator;
    }

    public async Task<ProductDTO> CreateAsync(CreateProductModel createProductModel, CancellationToken cancellationToken = default)
    {
        await _createValidator.EnsureValidAsync(createProductModel, cancellationToken: cancellationToken);

        var productGroupExist = await _productGroupRepository.ExistAsync(createProductModel.ProductGroupId, cancellationToken);

        if (!productGroupExist)
        {
            throw new ProductGroupNotFoundException(createProductModel.ProductGroupId);
        }

        var product = createProductModel.ToEntity();
        await _productRepository.CreateAsync(product, cancellationToken);

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

        return products.Select(x => x.ToDTO());
    }

    public async Task<ProductDTO> GetByIdAsync(Guid id, string languageCode, CancellationToken cancellationToken = default)
    {
        var product = await _productRepository.GetByIdAsync(id, languageCode, cancellationToken) ?? throw new ProductNotFoundException(id);
        return product.ToDTO();
    }

    public async Task<ProductDTO> UpdateAsync(Guid id, UpdateProductModel updateProductModel, CancellationToken cancellationToken = default)
    {
        await _updateValidator.EnsureValidAsync(updateProductModel, cancellationToken: cancellationToken);

        var product = await _productRepository.GetByIdAsync(id, cancellationToken) ?? throw new ProductNotFoundException(id);

        var productGroupExist = await _productGroupRepository.ExistAsync(updateProductModel.ProductGroupId, cancellationToken);
        if (!productGroupExist)
        {
            throw new ProductGroupNotFoundException(updateProductModel.ProductGroupId);
        }

        product = updateProductModel.UpdateEntity(product);

        await _productRepository.UpdateAsync(product, cancellationToken);
        return product.ToDTO();
    }
}
