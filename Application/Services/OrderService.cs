using Application.Abstractions.DTOs.Entities;
using Application.Abstractions.OrderAggregate;
using Application.Abstractions.OrderAggregate.Models;
using Application.Extensions;
using Application.Managers.OrderAudits;
using Application.Managers.OrderAudits.Models;
using Application.Managers.OrderTransactions;
using Application.Managers.OrderTransactions.Models;
using Application.Mappers;
using Domain.Abstractions;
using Domain.Entities;
using Domain.Exceptions;
using Domain.ValueModels;
using FluentValidation;

namespace Application.Services;

internal sealed class OrderService(IOrderRepository orderRepository,
    IProductSizeRepository productSizeRepository,
    IUserRepository userRepository,
    IOrderAuditManager orderAuditManager,
    IOrderTransactionManager orderTransactionManager,
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

            await AddTransactionToOrderAsync(order.Id, model.UserId, price, cancellationToken);
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
        var isExistedUser = await userRepository.IsExistedUserAsync(userId, cancellationToken);
        if (!isExistedUser) throw new EntityNotFoundException<User>(userId);
    }
    
    private async Task AddTransactionToOrderAsync(Guid orderId, Guid userId, decimal amount, CancellationToken cancellationToken = default)
    {
        var createTransaction = new AddTransactionToOrderModel
        {
            OrderId = orderId,
            UserId = userId,
            Amount = amount,
            TransactionType = TransactionType.Pay,
        };
        await orderTransactionManager.AddTransactionToOrderAsync(createTransaction, cancellationToken);
    }

    private async Task RevertOrderTransactionAsync(Order order, CancellationToken cancellationToken = default)
    {
        var revertOrderTransaction = new RevertOrderTransactionModel
        {
            OrderId = order.Id,
            UserId = order.UserId,
        };
        await orderTransactionManager.RevertOfferTransactionAsync(revertOrderTransaction, cancellationToken);
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
        
        await orderAuditManager.AddChangeOrderStatusAsync(item, cancellationToken);
    }
}