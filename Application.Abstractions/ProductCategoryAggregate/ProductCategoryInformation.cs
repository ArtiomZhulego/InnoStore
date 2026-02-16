namespace Application.Abstractions.ProductGroupAggregate;

public sealed class ProductCategoryInformation
{
    public Guid Id { get; set; }
    
    public required string Name { get; set; }
}
