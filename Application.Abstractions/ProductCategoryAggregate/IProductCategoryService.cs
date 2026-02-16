namespace Application.Abstractions.ProductGroupAggregate;

public interface IProductCategoryService
{
    public Task<ProductCategoryDTO> CreateAsync(CreateProductCategoryModel createProductGroupModel, CancellationToken cancellationToken = default);
    public Task<ProductCategoryDTO> UpdateAsync(Guid id,UpdateProductCategoryModel updateProductGroupModel, CancellationToken cancellationToken = default);
    public Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    public Task<ProductCategoryDTO> GetByIdAsync(Guid id, string languageCode, CancellationToken cancellationToken = default);
    public Task<IEnumerable<ProductCategoryDTO>> GetAllAsync(string languageCode, CancellationToken cancellationToken = default);
}
