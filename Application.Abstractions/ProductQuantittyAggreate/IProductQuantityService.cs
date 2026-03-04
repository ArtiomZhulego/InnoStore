using Application.Abstractions.DTOs.Entities;
using Application.Abstractions.ProductQuantittyAggreate.Models;

namespace Application.Abstractions.ProductQuantittyAggreate;

public interface IProductQuantityService
{
    Task<ProductQuantityTransactionDto> SupplyQuantityAsync(AddProductQuantityModel model, CancellationToken cancellationToken = default);
    Task<int> GetAvailableQuantityAsync(Guid productSizeId, CancellationToken cancellationToken = default);
    Task<IEnumerable<ProductQuantityTransactionDto>> GetChangeHistoryAsync(Guid productSizeId, CancellationToken cancellationToken = default);
}