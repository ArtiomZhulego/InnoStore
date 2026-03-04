using Domain.Entities;

namespace Domain.Abstractions;

public interface IProductQuantityTransactionRepository
{
    public Task AddAsync(ProductQuantityTransaction productQuantityTransaction, CancellationToken cancellationToken = default);
    public Task<IEnumerable<ProductQuantityTransaction>> GetByProductSizeIdAsync(Guid productSizeId, CancellationToken cancellationToken = default);
    public Task<int> GetTotalQuantityByProductSizeIdAsync(Guid productSizeId, CancellationToken cancellationToken = default);
}
