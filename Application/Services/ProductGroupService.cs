using Application.Abstractions.ProductGroupAggregate;
using Application.Extensions;
using Application.Mappers;
using Domain.Abstractions;
using Domain.Exceptions;

namespace Application.Services;

public class ProductGroupService(
    IProductGroupRepository productGroupRepository,
    IStorageService storageService
    ) : IProductCategoryService
{
    public async Task<ProductCategoryDTO> CreateAsync(CreateProductCategoryModel createProductGroupModel, CancellationToken cancellationToken = default)
    {
        var entity = createProductGroupModel.ToEntity();
        await productGroupRepository.CreateAsync(entity,cancellationToken);

        return entity.ToDTO();
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await productGroupRepository.GetByIdAsync(id, cancellationToken) ?? throw new ProductGroupNotFoundException(id);

        await productGroupRepository.DeleteAsync(entity, cancellationToken);
    }

    public async Task<IEnumerable<ProductCategoryDTO>> GetAllAsync(string languageCode, CancellationToken cancellationToken = default)
    {
        var groups = await productGroupRepository.GetAllAsync(languageCode, cancellationToken);

        var images = groups.SelectMany(x => x.Products).SelectMany(x => x.Colors).SelectMany(x => x.Images);

        await Parallel.ForEachAsync(images, cancellationToken, async (image, ct) =>
        {
            image.ImageUrl = await storageService.GetQuickAccessUrlAsync(image.ImageUrl, ct);
        });

        var response = groups.Select(x => x.ToDTO());

        return response;
    }

    public async Task<ProductCategoryDTO> GetByIdAsync(Guid id, string languageCode, CancellationToken cancellationToken = default)
    {
        var entity = await productGroupRepository.GetByIdAsync(id, languageCode, cancellationToken) ?? throw new ProductGroupNotFoundException(id);
        
        return entity.ToDTO();
    }

    public async Task<ProductCategoryDTO> UpdateAsync(Guid id, UpdateProductCategoryModel updateProductGroupModel, CancellationToken cancellationToken = default)
    {
        var entity = await productGroupRepository.GetByIdAsync(id, cancellationToken) ?? throw new ProductGroupNotFoundException(id);

        updateProductGroupModel.UpdateEntity(entity);

        await productGroupRepository.UpdateAsync(entity, cancellationToken);
        return entity.ToDTO();
    }
}
