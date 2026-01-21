using Domain.Entities;

namespace Domain.Abstractions;

public interface IProductRepository
{
    public Task<Product> CreateAsync(Product product, CancellationToken cancellationToken);
    public Task<IEnumerable<Product>> GetByGroupIdAsync(Guid groupId, string languageCode, CancellationToken cancellationToken);
    public Task<Product?> GetByIdAsync(Guid id, string languageCode, CancellationToken cancellationToken);
    public Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    public Task<IEnumerable<Product>> GetAllAsync(string languageCode, CancellationToken cancellationToken);
    public Task<Product> UpdateAsync(Product product, CancellationToken cancellationToken);
    public Task DeleteAsync(Product product, CancellationToken cancellationToken);
}
