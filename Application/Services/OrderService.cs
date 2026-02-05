using Application.Abstractions.DTOs.Entities;
using Application.Abstractions.OrderAggregate;
using Application.Abstractions.OrderAggregate.Models;
using Application.Extensions;
using Application.Mappers;
using Application.Services.Internal.OrderAudits;
using Application.Services.Internal.OrderAudits.Models;
using Domain.Abstractions;
using Domain.Entities;
using Domain.Exceptions;
using Domain.ValueModels;
using FluentValidation;

namespace Application.Services;

internal sealed class OrderService(IOrderRepository orderRepository,
    IProductSizeRepository productSizeRepository,
    IUserRepository userRepository,
    IInternalOrderAuditService internalOrderAuditService,
    IOrderTransactionsRepository orderTransactionsRepository,
    ITransactionRepository transactionRepository,
    IDatabaseTransactionManager transactionManager,
    IValidator validator) : IOrderService
{
    public async Task<OrderDto> CreateOrderAsync(CreateOrderModel model, CancellationToken cancellationToken = default)
    {
        await validator.EnsureValidAsync(model, cancellationToken: cancellationToken);
        await ValidateUserAsync(model.UserId, cancellationToken);
        
        var price = await GetProductPriceAsync(model.ProductSizeId, cancellationToken);

        // TODO: Validate user's amount
        
        var order = new Order
        {
            Id = Guid.NewGuid(),
            UserId = model.UserId,
            Price = price,
            Status = OrderStatus.Created
        };
        
        await transactionManager.BeginAsync(cancellationToken);
        try
        {
            await orderRepository.CreateAsync(order, cancellationToken);

            await AddTransactionToOrderAsync(order.Id, model.UserId, price, TransactionType.Pay, cancellationToken);
            await AddChangeOrderStatusAsync(model.UserId, order, cancellationToken);

            await transactionManager.CommitAsync(cancellationToken);
        }
        catch
        {
            await transactionManager.RollbackAsync(cancellationToken);
            throw;
        }

        return order.ToDto();
    }

    public async Task<OrderDto> CancelOrderAsync(CancelOrderModel cancelOrderModel, CancellationToken cancellationToken = default)
    {
        await validator.EnsureValidAsync(cancelOrderModel, cancellationToken: cancellationToken);
        var order = await orderRepository.GetByIdAsync(cancelOrderModel.OrderId, cancellationToken) ??
                    throw new EntityNotFoundException<Order>(cancelOrderModel.OrderId);

        order.Status = OrderStatus.Canceled;

        await transactionManager.BeginAsync(cancellationToken);
        try
        {
            await orderRepository.UpdateAsync(order, cancellationToken);
            await AddChangeOrderStatusAsync(cancelOrderModel.RevertedByUserId, order, cancellationToken);
            await RevertOrderTransactionAsync(order, cancellationToken);

            await transactionManager.CommitAsync(cancellationToken);
        }
        catch
        {
            await transactionManager.RollbackAsync(cancellationToken);
            throw;
        }

        return order.ToDto();
    }

    public async Task<OrderDto> GetOrderByIdAsync(Guid orderId, CancellationToken cancellationToken = default)
    {
        var order = await orderRepository.GetByIdAsync(orderId, cancellationToken) ??
                    throw new EntityNotFoundException<Order>(orderId);
        return order.ToDto();
    }

    public async Task<IEnumerable<OrderDto>> GetOrdersByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        await ValidateUserAsync(userId, cancellationToken);
        var orders = await orderRepository.GetByUserIdAsync(userId, cancellationToken);
        return orders.Select(order => order.ToDto());
    }

    private async Task ValidateUserAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        var isExistedUser = await userRepository.AnyAsync(userId, cancellationToken);
        if (!isExistedUser) throw new EntityNotFoundException<User>(userId);
    }
    
    private async Task<Transaction> AddTransactionToOrderAsync(Guid orderId,
        Guid userId,
        decimal amount,
        TransactionType transactionType,
        CancellationToken cancellationToken = default)
    {
        var transaction = new Transaction()
        {
            UserId = userId,
            Amount = amount,
            Type = transactionType
        };
        
        await transactionRepository.AddAsync(transaction, cancellationToken);
        
        var orderTransaction = new OrderTransaction()
        {
            OrderId = orderId,
            TransactionId = transaction.Id,
        };
        
        await orderTransactionsRepository.AddAsync(orderTransaction, cancellationToken);
        
        return transaction;
    }

    private async Task<Transaction> RevertOrderTransactionAsync(Order order, CancellationToken cancellationToken = default)
    {
        var existingTransactions = await orderTransactionsRepository.GetByOrderId(order.Id, cancellationToken);

        decimal totalAmount = 0;
        var items = existingTransactions
            .Select(item => item.Transaction!);
        
        foreach (var transaction in items)
        {
            if (transaction.Type == TransactionType.Refund) totalAmount -= Math.Abs(transaction.Amount);
            else if (transaction.Type == TransactionType.Pay)  totalAmount += Math.Abs(transaction.Amount);
        }

        if (totalAmount <= 0)
        {
            throw new InvalidOperationException($"Order {order.Id} has no funds to revert.");
        }

        var refundTransaction = new Transaction
        {
            Id = Guid.NewGuid(),
            UserId = order.UserId,
            Amount = totalAmount,
            Type = TransactionType.Refund
        };

            await transactionRepository.AddAsync(refundTransaction, cancellationToken);

            var orderTransactionLink = new OrderTransaction
            {
                OrderId = order.Id,
                TransactionId = refundTransaction.Id
            };

            await orderTransactionsRepository.AddAsync(orderTransactionLink, cancellationToken);

        return refundTransaction;
    }

    private async Task<decimal> GetProductPriceAsync(Guid productSizeId, CancellationToken cancellationToken = default)
    {
        var productSize =
            await productSizeRepository.GetProductSizeByIdAsync(productSizeId, cancellationToken) ??
            throw new EntityNotFoundException<ProductSize>(productSizeId);

        var price =
            productSize.Product?.Price ??
            throw new EntityNotFoundException<Product>(productSize.ProductId);

        return price;
    }
    
    private async Task AddChangeOrderStatusAsync(Guid userId, Order order, CancellationToken cancellationToken = default)
    {
        var item = new AddChangeOrderStatusModel
        {
            UserId = userId,
            OrderId = order.Id,
            OrderStatus = order.Status,
        };
        
        await internalOrderAuditService.AddChangeOrderStatusAsync(item, cancellationToken);
    }
}