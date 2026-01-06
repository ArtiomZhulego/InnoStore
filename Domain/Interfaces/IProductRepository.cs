using Domain.Entities;

namespace Domain.Interfaces;

public interface IProductRepository
{
    public Task<Product> CreateAsync(Product product, CancellationToken cancellationToken);
    public Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    public Task<IEnumerable<Product>> GetAllAsync(CancellationToken cancellationToken);
    public Task<Product> UpdateAsync(Product product, CancellationToken cancellationToken);
}
