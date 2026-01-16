namespace Application.Abstractions.ProductGroupAggregate;

public interface IProductGroupService
{
    public Task<ProductGroupDTO> CreateAsync(CreateProductGroupModel createProductGroupModel, CancellationToken cancellationToken = default);
    public Task<ProductGroupDTO> UpdateAsync(Guid id,UpdateProductGroupModel updateProductGroupModel, CancellationToken cancellationToken = default);
    public Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    public Task<ProductGroupDTO> GetByIdAsync(Guid id, string languageCode, CancellationToken cancellationToken = default);
    public Task<IEnumerable<ProductGroupDTO>> GetAllAsync(string languageCode, CancellationToken cancellationToken = default);
}
