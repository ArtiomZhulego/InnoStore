using Domain.Entities;

namespace Application.Services.Internal.ProductQuantities;

internal interface IInternalProductQuantityService
{
    public Task<int> GetAvailableQuantityAsync(Guid productSizeId, CancellationToken cancellationToken = default);
    
    Task<ProductQuantityTransaction> AddQuantityAsync(Guid productSizeId, Guid adminUserId, int quantity, CancellationToken cancellationToken = default);
    
    Task<ProductQuantityTransaction> ReduceQuantityForOrderAsync(Guid orderId, Guid productSizeId, Guid userId, int quantity, CancellationToken cancellationToken = default);
    
    Task RevertQuantityForOrderAsync(Guid orderId,  Guid revertedByUserId, CancellationToken cancellationToken = default);
}