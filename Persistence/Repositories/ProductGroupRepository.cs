using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

internal class ProductGroupRepository : IProductGroupRepository
{
    private readonly InnoStoreContext _context;

    public ProductGroupRepository(InnoStoreContext context)
    {
        _context = context;
    }

    public async Task<bool> ExistAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.ProductGroups.AnyAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<ProductGroup>> GetAllAsync(string languageCode, CancellationToken cancellationToken)
    {
        return await _context.ProductGroups
            .Include(x => x.Localizations.Where(x => x.LanguageISOCode == languageCode))
            .ToListAsync(cancellationToken);
    }

    public async Task<ProductGroup?> GetByIdAsync(Guid id, string languageCode, CancellationToken cancellationToken)
    {
        return await _context.ProductGroups
            .Include(x => x.Localizations.Where(x => x.LanguageISOCode == languageCode))
            .FirstOrDefaultAsync(x => x.Id == id,cancellationToken);
    }
}
