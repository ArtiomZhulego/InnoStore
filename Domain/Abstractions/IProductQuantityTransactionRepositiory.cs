using Domain.Entities;

namespace Domain.Abstractions;

public interface IProductQuantityTransactionRepositiory
{
    public Task AddAsync(ProductQuantityTransaction productQuantityTransaction, CancellationToken cancellationToken = default);

    public Task<IEnumerable<ProductQuantityTransaction>> GetByProductSizeIdAsync(Guid productSizeId, CancellationToken cancellationToken = default);
    public Task<ProductQuantityTransaction?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
}
