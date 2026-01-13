namespace Application.Abstractions.ProductAggregate;

public interface IProductService
{
    public Task<ProductDTO> GetByIdAsync(Guid id, string languageCode, CancellationToken cancellationToken = default);
    public Task<IEnumerable<ProductDTO>> GetByGroupAsync(Guid groupId, string languageCode, CancellationToken cancellationToken = default);
    public Task<ProductDTO> CreateAsync(CreateProductModel createProductModel, CancellationToken cancellationToken = default);
    public Task<ProductDTO> UpdateAsync(Guid id, UpdateProductModel updateProductModel, CancellationToken cancellationToken = default);
    public Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
