using Domain.Abstractions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

internal sealed class ProductSizeRepository(InnoStoreContext context) : IProductSizeRepository
{
    public async Task<ProductSize?> GetProductSizeByIdAsync(Guid productSizeId, CancellationToken cancellationToken = default) =>
        await context.ProductSizes.FirstOrDefaultAsync(item => item.Id == productSizeId, cancellationToken);
}
