using Domain.Abstractions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

internal sealed class ProductQuantityTransactionRepository(InnoStoreContext context) : IProductQuantityTransactionRepository
{
    public async Task AddAsync(ProductQuantityTransaction productQuantityTransaction, CancellationToken cancellationToken = default)
    {
        context.ProductQuantityTransactions.Add(productQuantityTransaction);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task<IEnumerable<ProductQuantityTransaction>> GetByProductSizeIdAsync(Guid productSizeId, CancellationToken cancellationToken = default) =>
        await context.ProductQuantityTransactions
            .Where(item => item.ProductSizeId == productSizeId)
            .ToListAsync(cancellationToken);

    public async Task<int> GetTotalQuantityByProductSizeIdAsync(Guid productSizeId, CancellationToken cancellationToken = default) =>
        await context.ProductQuantityTransactions
            .Where(item => item.ProductSizeId == productSizeId)
            .SumAsync(item => item.OperationAmount, cancellationToken);
}
