using Domain.Abstractions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

internal class ProductRepository : IProductRepository
{
    private readonly InnoStoreContext _context;

    public ProductRepository(InnoStoreContext context)
    {
        _context = context;
    }

    public async Task<Product> CreateAsync(Product product, CancellationToken cancellationToken)
    {
        _context.Products.Add(product);
        await _context.SaveChangesAsync(cancellationToken);
        return product;
    }

    public Task DeleteAsync(Product product, CancellationToken cancellationToken)
    {
        _context.Products.Remove(product);
        return _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<IEnumerable<Product>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.Products
            .Include(x => x.Localizations)
            .Include(x => x.Colors)
                .ThenInclude(x => x.Images)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Product>> GetByGroupIdAsync(Guid groupId, string languageCode, CancellationToken cancellationToken)
    {
        return await _context.Products
            .Where(x => x.ProductCategoryId == groupId)
            .Include(x => x.Localizations.Where(x => x.LanguageISOCode == languageCode))
            .Include(x => x.ProductCategory)
                .ThenInclude(x => x.Localizations.Where(x => x.LanguageISOCode == languageCode))
            .Include(x => x.Sizes)
                .ThenInclude(x => x.Localizations.Where(x => x.LanguageISOCode == languageCode))
            .Include(x => x.Colors)
                .ThenInclude(x => x.Images)
            .ToListAsync(cancellationToken);
    }

    public async Task<Product?> GetByIdAsync(Guid id, string languageCode, CancellationToken cancellationToken)
    {
        return await _context.Products
            .Include(x => x.Localizations.Where(x => x.LanguageISOCode == languageCode))
            .Include(x => x.ProductCategory)
                .ThenInclude(x => x.Localizations.Where(x => x.LanguageISOCode == languageCode))
             .Include(x => x.Sizes)
                .ThenInclude(x => x.Localizations.Where(x => x.LanguageISOCode == languageCode))
             .Include(x => x.Colors)
                .ThenInclude(x => x.Images)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Products
            .Include(x => x.Localizations)
            .Include(x => x.ProductCategory)
                .ThenInclude(x => x.Localizations)
            .Include(x => x.Sizes)
                    .ThenInclude(x => x.Localizations)
            .Include(x => x.Colors)
                .ThenInclude(x => x.Images)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<Product> UpdateAsync(Product product, CancellationToken cancellationToken)
    {
        _context.Products.Update(product);
        await _context.SaveChangesAsync(cancellationToken);
        return product;
    }
}
