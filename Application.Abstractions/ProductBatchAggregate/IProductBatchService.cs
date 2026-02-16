using Application.Abstractions.ProductAggregate;

namespace Application.Abstractions.ProductBatchAggregate;

public interface IProductBatchService
{
    public Task<IEnumerable<ProductDTO>> CreateBatchAsync(CreateProductBatchModel createProductModel, CancellationToken cancellationToken = default);
    public Task<IEnumerable<ProductDTO>> UpdateBatchAsync(Guid id, UpdateProductBatchModel updateProductBatchModel, CancellationToken cancellationToken = default);
}
