using Domain.Abstractions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

internal sealed class ProductQuantityTransactionRepositiory(InnoStoreContext context) : IProductQuantityTransactionRepositiory
{
    public async Task AddAsync(ProductQuantityTransaction productQuantityTransaction, CancellationToken cancellationToken = default)
    {
        context.ProductQuantityTransactions.Add(productQuantityTransaction);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task<ProductQuantityTransaction?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
        await context.ProductQuantityTransactions.FirstOrDefaultAsync(item => item.Id == id, cancellationToken);

    public async Task<IEnumerable<ProductQuantityTransaction>> GetByProductSizeIdAsync(Guid productSizeId, CancellationToken cancellationToken = default) =>
        await context.ProductQuantityTransactions.Where(item => item.ProductSizeId == productSizeId).ToListAsync(cancellationToken);
}
