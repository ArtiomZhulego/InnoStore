using Domain.Entities;

namespace Domain.Interfaces;

public interface IProductGroupRepository
{
    public Task<bool> ExistAsync(Guid id, CancellationToken cancellationToken);
    public Task<ProductGroup?> GetByIdAsync(Guid id, string languageCode, CancellationToken cancellationToken);
    public Task<IEnumerable<ProductGroup>> GetAllAsync(string languageCode, CancellationToken cancellationToken);
}
