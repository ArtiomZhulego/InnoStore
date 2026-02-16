using Application.Abstractions.ProductAggregate;

namespace Application.Abstractions.ProductGroupAggregate;

public sealed class ProductCategoryDTO
{
    public Guid Id { get; set; }
    
    public string Name { get; set; } = string.Empty;
    
    public IEnumerable<ProductInformation> Products { get; set; } = [];
}
