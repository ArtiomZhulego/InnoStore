namespace Application.Abstractions.ProductImageAggregate;

public class UpdateProductImageModel
{
    public required string ImageUrl { get; set; }
    public required int OrderNumber { get; set; }
}
