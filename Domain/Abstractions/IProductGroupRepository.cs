using Domain.Entities;

namespace Domain.Abstractions;

public interface IProductGroupRepository
{
    public Task<ProductGroup> CreateAsync(ProductGroup productGroup, CancellationToken cancellationToken);
    public Task<ProductGroup> UpdateAsync(ProductGroup productGroup, CancellationToken cancellationToken);
    public Task DeleteAsync(ProductGroup productGroup, CancellationToken cancellationToken);
    public Task<bool> ExistAsync(Guid id, CancellationToken cancellationToken);
    public Task<ProductGroup?> GetByIdAsync(Guid id, string languageCode, CancellationToken cancellationToken);
    public Task<ProductGroup?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    public Task<IEnumerable<ProductGroup>> GetAllAsync(string languageCode, CancellationToken cancellationToken);
    public Task<IEnumerable<ProductGroup>> GetAllAsync( CancellationToken cancellationToken);
}
