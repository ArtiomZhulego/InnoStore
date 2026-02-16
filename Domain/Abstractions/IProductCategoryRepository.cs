using Domain.Entities;

namespace Domain.Abstractions;

public interface IProductCategoryRepository
{
    public Task<ProductCategory> CreateAsync(ProductCategory productCategory, CancellationToken cancellationToken);
    public Task<ProductCategory> UpdateAsync(ProductCategory productCategory, CancellationToken cancellationToken);
    public Task DeleteAsync(ProductCategory productCategory, CancellationToken cancellationToken);
    public Task<bool> ExistAsync(Guid id, CancellationToken cancellationToken);
    public Task<ProductCategory?> GetByIdAsync(Guid id, string languageCode, CancellationToken cancellationToken);
    public Task<ProductCategory?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    public Task<IEnumerable<ProductCategory>> GetAllAsync(string languageCode, CancellationToken cancellationToken);
    public Task<IEnumerable<ProductCategory>> GetAllAsync( CancellationToken cancellationToken);
}
