namespace Application.Abstractions.ProductGroupAggregate;

public sealed class ProductGroupInformation
{
    public Guid Id { get; set; }
    
    public required string Name { get; set; }
}
