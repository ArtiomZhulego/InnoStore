using Domain.Abstractions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

internal class ProductCategoryRepository : IProductCategoryRepository
{
    private readonly InnoStoreContext _context;

    public ProductCategoryRepository(InnoStoreContext context)
    {
        _context = context;
    }

    public async Task<ProductCategory> CreateAsync(ProductCategory productGroup, CancellationToken cancellationToken)
    {
        _context.ProductCategories.Add(productGroup);
        await _context.SaveChangesAsync(cancellationToken);
        return productGroup;
    }

    public async Task DeleteAsync(ProductCategory productGroup, CancellationToken cancellationToken)
    {
        _context.ProductCategories.Remove(productGroup);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> ExistAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.ProductCategories.AnyAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<ProductCategory>> GetAllAsync(string languageCode, CancellationToken cancellationToken)
    {
        return await _context.ProductCategories
            .Include(x => x.Localizations.Where(x => x.LanguageISOCode == languageCode))
            .Include(x => x.Products)
                .ThenInclude(x => x.Localizations.Where(x => x.LanguageISOCode == languageCode))
            .Include(x => x.Products)
                .ThenInclude(x => x.Colors)
                    .ThenInclude(x => x.Images)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<ProductCategory>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.ProductCategories
            .Include(x => x.Localizations)
            .ToListAsync(cancellationToken);
    }

    public async Task<ProductCategory?> GetByIdAsync(Guid id, string languageCode, CancellationToken cancellationToken)
    {
        return await _context.ProductCategories
            .Include(x => x.Localizations.Where(x => x.LanguageISOCode == languageCode))
            .Include(x => x.Products)
                .ThenInclude(x => x.Localizations.Where(x => x.LanguageISOCode == languageCode))
            .FirstOrDefaultAsync(x => x.Id == id,cancellationToken);
    }

    public async Task<ProductCategory?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.ProductCategories
            .Include(x => x.Localizations)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<ProductCategory> UpdateAsync(ProductCategory productGroup, CancellationToken cancellationToken)
    {
        _context.ProductCategories.Update(productGroup);
        await _context.SaveChangesAsync(cancellationToken);
        return productGroup;
    }
}
