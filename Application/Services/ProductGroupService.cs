using Application.Abstractions.ProductGroupAggregate;
using Application.Extensions;
using Application.Mappers;
using Domain.Exceptions;
using Domain.Interfaces;
using FluentValidation;

namespace Application.Services;

public class ProductGroupService : IProductGroupService
{
    private readonly IProductGroupRepository _productGroupRepository;
    private readonly IValidator<CreateProductGroupModel> _createValidator;
    private readonly IValidator<UpdateProductGroupModel> _updateValidator;

    public ProductGroupService(IProductGroupRepository productGroupRepository, IValidator<CreateProductGroupModel> createValidator, IValidator<UpdateProductGroupModel> updateValidator)
    {
        _productGroupRepository = productGroupRepository;
        _createValidator = createValidator;
        _updateValidator = updateValidator;
    }

    public async Task<ProductGroupDTO> CreateAsync(CreateProductGroupModel createProductGroupModel, CancellationToken cancellationToken = default)
    {
        await _createValidator.EnsureValidAsync(createProductGroupModel, cancellationToken);

        var entity = createProductGroupModel.ToEntity();
        await _productGroupRepository.CreateAsync(entity,cancellationToken);

        return entity.ToDTO();
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await _productGroupRepository.GetByIdAsync(id, cancellationToken) ?? throw new ProductGroupNotFoundException(id);

        await _productGroupRepository.DeleteAsync(entity, cancellationToken);
    }

    public async Task<IEnumerable<ProductGroupDTO>> GetAllAsync(string languageCode, CancellationToken cancellationToken = default)
    {
        var groups = await _productGroupRepository.GetAllAsync(languageCode, cancellationToken);
        return groups.Select(x => x.ToDTO());
    }

    public async Task<ProductGroupDTO> GetByIdAsync(Guid id, string languageCode, CancellationToken cancellationToken = default)
    {
        var entity = await _productGroupRepository.GetByIdAsync(id, languageCode, cancellationToken) ?? throw new ProductGroupNotFoundException(id);
        
        return entity.ToDTO();
    }

    public async Task<ProductGroupDTO> UpdateAsync(Guid id, UpdateProductGroupModel updateProductGroupModel, CancellationToken cancellationToken = default)
    {
        await _updateValidator.EnsureValidAsync(updateProductGroupModel, cancellationToken);

        var entity = await _productGroupRepository.GetByIdAsync(id, cancellationToken) ?? throw new ProductGroupNotFoundException(id);

        entity = updateProductGroupModel.UpdateEntity(entity);

        await _productGroupRepository.UpdateAsync(entity, cancellationToken);
        return entity.ToDTO();
    }
}
