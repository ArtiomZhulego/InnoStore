namespace Application.Abstractions.ProductAggregate;

public sealed class ProductInformation
{
    public Guid Id { get; set; }
    
    public required string Name { get; set; }
    
    public required string Description { get; set; }
    
    public required decimal Price { get; set; }
}
