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

    public async Task<ProductGroup> CreateAsync(ProductGroup productGroup, CancellationToken cancellationToken)
    {
        _context.ProductGroups.Add(productGroup);
        await _context.SaveChangesAsync(cancellationToken);
        return productGroup;
    }

    public async Task DeleteAsync(ProductGroup productGroup, CancellationToken cancellationToken)
    {
        _context.ProductGroups.Remove(productGroup);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> ExistAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.ProductGroups.AnyAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<ProductGroup>> GetAllAsync(string languageCode, CancellationToken cancellationToken)
    {
        return await _context.ProductGroups
            .Include(x => x.Localizations.Where(x => x.LanguageISOCode == languageCode))
            .Include(x => x.Products)
                .ThenInclude(x => x.Localizations.Where(x => x.LanguageISOCode == languageCode))
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<ProductGroup>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.ProductGroups
            .Include(x => x.Localizations)
            .Include(x => x.Products)
                .ThenInclude(x => x.Localizations)
            .ToListAsync(cancellationToken);
    }

    public async Task<ProductGroup?> GetByIdAsync(Guid id, string languageCode, CancellationToken cancellationToken)
    {
        return await _context.ProductGroups
            .Include(x => x.Localizations.Where(x => x.LanguageISOCode == languageCode))
            .Include(x => x.Products)
                .ThenInclude(x => x.Localizations.Where(x => x.LanguageISOCode == languageCode))
            .FirstOrDefaultAsync(x => x.Id == id,cancellationToken);
    }

    public async Task<ProductGroup?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.ProductGroups
            .Include(x => x.Localizations)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<ProductGroup> UpdateAsync(ProductGroup productGroup, CancellationToken cancellationToken)
    {
        _context.ProductGroups.Update(productGroup);
        await _context.SaveChangesAsync(cancellationToken);
        return productGroup;
    }
}
