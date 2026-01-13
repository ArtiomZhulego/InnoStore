using Domain.Entities;
using Domain.Interfaces;
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

    public async Task<IEnumerable<Product>> GetAllAsync(string languageCode, CancellationToken cancellationToken)
    {
        return await _context.Products
            .Include(x => x.Localizations.Where(x => x.LanguageISOCode == languageCode))
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Product>> GetByGroupIdAsync(Guid groupId, string languageCode, CancellationToken cancellationToken)
    {
        return await _context.Products
            .Where(x => x.ProductGroupId == groupId)
            .Include(x => x.Localizations.Where(x => x.LanguageISOCode == languageCode))
            .ToListAsync(cancellationToken);
    }

    public async Task<Product?> GetByIdAsync(Guid id, string languageCode, CancellationToken cancellationToken)
    {
        return await _context.Products
            .Include(x => x.Localizations.Where(x => x.LanguageISOCode == languageCode))
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Products
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<Product> UpdateAsync(Product product, CancellationToken cancellationToken)
    {
        _context.Products.Update(product);
        await _context.SaveChangesAsync(cancellationToken);
        return product;
    }
}
