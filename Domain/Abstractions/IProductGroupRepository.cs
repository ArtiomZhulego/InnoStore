using Domain.Entities;

namespace Domain.Abstractions;

public interface IProductGroupRepository
{
    public Task<ProductCategory> CreateAsync(ProductCategory productGroup, CancellationToken cancellationToken);
    public Task<ProductCategory> UpdateAsync(ProductCategory productGroup, CancellationToken cancellationToken);
    public Task DeleteAsync(ProductCategory productGroup, CancellationToken cancellationToken);
    public Task<bool> ExistAsync(Guid id, CancellationToken cancellationToken);
    public Task<ProductCategory?> GetByIdAsync(Guid id, string languageCode, CancellationToken cancellationToken);
    public Task<ProductCategory?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    public Task<IEnumerable<ProductCategory>> GetAllAsync(string languageCode, CancellationToken cancellationToken);
    public Task<IEnumerable<ProductCategory>> GetAllAsync( CancellationToken cancellationToken);
}
