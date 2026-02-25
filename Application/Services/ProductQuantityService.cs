using Application.Abstractions.DTOs.Entities;
using Application.Abstractions.ProductQuantittyAggreate;
using Application.Abstractions.ProductQuantittyAggreate.Models;
using Application.Mapper;
using Application.Services.Internal.ProductQuantities;

namespace Application.Services;

internal sealed class ProductQuantityService(IInternalProductQuantityService internalProductQuantityService) : IProductQuantityService
{
    public async Task<int> GetAvailableQuantityAsync(Guid productSizeId, CancellationToken cancellationToken = default)
    {
        return await internalProductQuantityService.GetAvailableQuantityAsync(productSizeId, cancellationToken);
    }

    public async Task<IEnumerable<ProductQuantityTransactionDto>> GetChangeHistoryAsync(Guid productSizeId, CancellationToken cancellationToken = default)
    {
        var history = await internalProductQuantityService.GetChangeHistoryAsync(productSizeId, cancellationToken);
        return history.ToDto();
    }

    public async Task<ProductQuantityTransactionDto> SupplyQuantityAsync(AddProductQuantityModel model, CancellationToken cancellationToken = default)
    {
        var transaction = await internalProductQuantityService.AddQuantityAsync(
            model.ProductSizeId,
            model.UserId,
            model.Quantity,
            cancellationToken);

        return transaction.ToDto();
    }
}