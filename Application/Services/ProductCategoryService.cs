using Application.Abstractions.ProductCategoryAggregate;
using Application.Extensions;
using Application.Mappers;
using Domain.Abstractions;
using Domain.Exceptions;

namespace Application.Services;

public class ProductCategoryService(
    IProductCategoryRepository productCategoryRepository,
    IStorageService storageService
    ) : IProductCategoryService
{
    public async Task<ProductCategoryDTO> CreateAsync(CreateProductCategoryModel createProductCategoryModel, CancellationToken cancellationToken = default)
    {
        var entity = createProductCategoryModel.ToEntity();
        await productCategoryRepository.CreateAsync(entity,cancellationToken);

        return entity.ToDTO();
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await productCategoryRepository.GetByIdAsync(id, cancellationToken) ?? throw new ProductCategoryNotFoundException(id);

        await productCategoryRepository.DeleteAsync(entity, cancellationToken);
    }

    public async Task<IEnumerable<ProductCategoryDTO>> GetAllAsync(string languageCode, CancellationToken cancellationToken = default)
    {
        var groups = await productCategoryRepository.GetAllAsync(languageCode, cancellationToken);

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
        var entity = await productCategoryRepository.GetByIdAsync(id, languageCode, cancellationToken) ?? throw new ProductCategoryNotFoundException(id);
        
        return entity.ToDTO();
    }

    public async Task<ProductCategoryDTO> UpdateAsync(Guid id, UpdateProductCategoryModel updateProductCategoryModel, CancellationToken cancellationToken = default)
    {
        var entity = await productCategoryRepository.GetByIdAsync(id, cancellationToken) ?? throw new ProductCategoryNotFoundException(id);

        updateProductCategoryModel.UpdateEntity(entity);

        await productCategoryRepository.UpdateAsync(entity, cancellationToken);
        return entity.ToDTO();
    }
}
