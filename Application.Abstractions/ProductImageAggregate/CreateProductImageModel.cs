namespace Application.Abstractions.ProductImageAggregate;

public sealed class CreateProductImageModel
{
    public required string ImageUrl { get; set; }
    public required int OrderNumber { get; set; }
}
