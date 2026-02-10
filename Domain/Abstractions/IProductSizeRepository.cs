using Domain.Entities;

namespace Domain.Abstractions;

public interface IProductSizeRepository
{
    public Task<ProductSize?> GetProductSizeByIdAsync(Guid productSizeId, CancellationToken cancellationToken = default);
}