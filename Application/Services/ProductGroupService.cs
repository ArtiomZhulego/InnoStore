using Application.Abstractions.ProductGroupAggregate;
using Application.Extensions;
using Application.Mappers;
using Domain.Abstractions;
using Domain.Exceptions;
using FluentValidation;

namespace Application.Services;

public class ProductGroupService(
    IProductGroupRepository productGroupRepository,
    IStorageService storageService,
    IValidator<CreateProductGroupModel> createValidator,
    IValidator<UpdateProductGroupModel> updateValidator)
    : IProductGroupService
{
    public async Task<ProductGroupDTO> CreateAsync(CreateProductGroupModel createProductGroupModel, CancellationToken cancellationToken = default)
    {
        await createValidator.EnsureValidAsync(createProductGroupModel, cancellationToken);

        var entity = createProductGroupModel.ToEntity();
        await productGroupRepository.CreateAsync(entity,cancellationToken);

        return entity.ToDTO();
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await productGroupRepository.GetByIdAsync(id, cancellationToken) ?? throw new ProductGroupNotFoundException(id);

        await productGroupRepository.DeleteAsync(entity, cancellationToken);
    }

    public async Task<IEnumerable<ProductGroupDTO>> GetAllAsync(string languageCode, CancellationToken cancellationToken = default)
    {
        var groups = await productGroupRepository.GetAllAsync(languageCode, cancellationToken);

        var images = groups.SelectMany(x => x.Products).SelectMany(x => x.Images);

        await Parallel.ForEachAsync(images, cancellationToken, async (image, ct) =>
        {
            image.ImageUrl = await storageService.GetQuickAccessUrlAsync(image.ImageUrl);
        });

        var response = groups.Select(x => x.ToDTO());

        return response;
    }

    public async Task<ProductGroupDTO> GetByIdAsync(Guid id, string languageCode, CancellationToken cancellationToken = default)
    {
        var entity = await productGroupRepository.GetByIdAsync(id, languageCode, cancellationToken) ?? throw new ProductGroupNotFoundException(id);
        
        return entity.ToDTO();
    }

    public async Task<ProductGroupDTO> UpdateAsync(Guid id, UpdateProductGroupModel updateProductGroupModel, CancellationToken cancellationToken = default)
    {
        await updateValidator.EnsureValidAsync(updateProductGroupModel, cancellationToken);

        var entity = await productGroupRepository.GetByIdAsync(id, cancellationToken) ?? throw new ProductGroupNotFoundException(id);

        updateProductGroupModel.UpdateEntity(entity);

        await productGroupRepository.UpdateAsync(entity, cancellationToken);
        return entity.ToDTO();
    }
}
