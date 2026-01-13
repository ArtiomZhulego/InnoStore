namespace Application.Abstractions.ProductGroupAggregate;

public interface IProductGroupService
{
    public Task<ProductGroupDTO> GetByIdAsync(Guid id);
    public Task<IEnumerable<ProductGroupDTO>> GetAllAsync();
}
