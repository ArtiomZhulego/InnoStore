using Domain.Abstractions;
using Domain.Entities;
using Domain.ValueModels;

namespace Application.Services.Internal.ProductQuantities;

internal sealed class InternalProductQuantityService(
    IProductQuantityTransactionRepository quantityRepository,
    IOrderProductQuantityTransactionRepository orderQuantityRepository,
    IDatabaseTransactionManager transactionManager)
    : IInternalProductQuantityService
{
    public async Task<int> GetAvailableQuantityAsync(Guid productSizeId, CancellationToken cancellationToken = default) =>
        await quantityRepository.GetTotalQuantityByProductSizeIdAsync(productSizeId, cancellationToken);

    public async Task<ProductQuantityTransaction> AddQuantityAsync(Guid productSizeId, Guid adminUserId, int quantity, CancellationToken cancellationToken = default)
    {
        if (quantity <= 0)
            throw new ArgumentException("Quantity to supply must be greater than zero.", nameof(quantity));

        var transaction = new ProductQuantityTransaction
        {
            Id = Guid.NewGuid(),
            ProductSizeId = productSizeId,
            UserId = adminUserId,
            OperationAmount = quantity,
            EventType = ProductQuantityTransactionType.Add
        };

        await quantityRepository.AddAsync(transaction, cancellationToken);
        return transaction;
    }

    public async Task<ProductQuantityTransaction> ReduceQuantityForOrderAsync(Guid orderId, Guid productSizeId, Guid userId, int quantity, CancellationToken cancellationToken = default)
    {
        var transaction = new ProductQuantityTransaction
        {
            Id = Guid.NewGuid(),
            ProductSizeId = productSizeId,
            UserId = userId,
            OperationAmount = -Math.Abs(quantity), 
            EventType = ProductQuantityTransactionType.Reduce 
        };

        await using var dbTransaction = await transactionManager.BeginTransactionAsync(cancellationToken);
        await quantityRepository.AddAsync(transaction, cancellationToken);

        var orderLink = new OrderProductQuantityTransaction
        {
            Id = Guid.NewGuid(),
            OrderId = orderId,
            ProductQuantityTransactionId = transaction.Id
        };

        await orderQuantityRepository.AddAsync(orderLink, cancellationToken);

        await dbTransaction.CommitAsync(cancellationToken);
        return transaction;
    }

    public async Task RevertQuantityForOrderAsync(
        Guid orderId, 
        Guid revertedByUserId, 
        CancellationToken cancellationToken = default)
         {
        var existingLinks = await orderQuantityRepository.GetByOrderIdAsync(orderId, cancellationToken);

        var linksToRevert = existingLinks
            .Where(link => link.ProductQuantityTransaction is { EventType: ProductQuantityTransactionType.Reduce })
            .ToList();
        
        if (!linksToRevert.Any()) return;
        
        await using var dbTransaction = await transactionManager.BeginTransactionAsync(cancellationToken);
        foreach (var link in linksToRevert)
        {
            var originalTx = link.ProductQuantityTransaction!;

            var refundTransaction = new ProductQuantityTransaction
            {
                Id = Guid.NewGuid(),
                ProductSizeId = originalTx.ProductSizeId,
                UserId = revertedByUserId,
                OperationAmount = Math.Abs(originalTx.OperationAmount),
                EventType = ProductQuantityTransactionType.Add
            };

            await quantityRepository.AddAsync(refundTransaction, cancellationToken);

            var newOrderLink = new OrderProductQuantityTransaction
            {
                Id = Guid.NewGuid(),
                OrderId = orderId,
                ProductQuantityTransactionId = refundTransaction.Id
            };

            await orderQuantityRepository.AddAsync(newOrderLink, cancellationToken);
        }
        await dbTransaction.CommitAsync(cancellationToken);
    }
}