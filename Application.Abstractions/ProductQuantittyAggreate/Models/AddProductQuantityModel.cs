namespace Application.Abstractions.ProductQuantittyAggreate.Models;

public sealed record AddProductQuantityModel
{
    public required Guid ProductSizeId { get; init; }
    public required Guid UserId { get; init; }
    public required int Quantity { get; init; }
}
