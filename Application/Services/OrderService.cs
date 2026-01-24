using Application.Abstractions.DTOs.Entities;
using Application.Abstractions.OrderAggregate;
using Application.Abstractions.OrderAggregate.Models;
using Application.Managers.OrderAudits;
using Application.Managers.OrderAudits.Models;
using Application.Managers.OrderTransactions;
using Application.Managers.OrderTransactions.Models;
using Application.Mappers;
using Domain.Abstractions;
using Domain.Entities;
using Domain.ValueModels;

namespace Application.Services;

internal sealed class OrderService(IOrderRepository orderRepository,
    IProductSizeRepository productSizeRepository,
    IOrderAuditManager orderAuditManager,
    IOrderTransactionManager orderTransactionManager
    ) : IOrderService
{
    public async Task<OrderDto> CreateOrderAsync(CreateOrderModel model, CancellationToken cancellationToken = default)
    {
        var price = await GetProductPriceAsync(model.ProductSizeId, cancellationToken);

        var order = new Order
        {
            Id = Guid.NewGuid(),
            UserId = model.UserId,
            Price = price,
            Status = OrderStatus.Created
        };
        var createdOrder = await orderRepository.CreateAsync(order, cancellationToken);
        
        await AddTransactionToOrderAsync(order.Id, model.UserId, price, cancellationToken);
        await AddChangeOrderStatusAsync(model.UserId, order, cancellationToken);
        
        return createdOrder.ToDto();
    }

    public async Task<OrderDto?> CancelOrderAsync(CancelOrderModel cancelOrderModel, CancellationToken cancellationToken = default)
    {
        var order = await orderRepository.GetByIdAsync(cancelOrderModel.OrderId, cancellationToken);
        if (order == null) throw new ArgumentNullException(); // TODO

        order.Status = OrderStatus.Canceled;
        await orderRepository.UpdateAsync(order, cancellationToken);
        
        await AddChangeOrderStatusAsync(cancelOrderModel.RevertedByUserId, order, cancellationToken);
        await RevertOrderTransactionAsync(order, cancellationToken);
        
        return order.ToDto();
    }

    public async Task<OrderDto?> GetOrderByIdAsync(Guid orderId, CancellationToken cancellationToken = default)
    {
        var order = await orderRepository.GetByIdAsync(orderId, cancellationToken);
        return order?.ToDto();
    }

    public async Task<IEnumerable<OrderDto>> GetOrdersByUserAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        var orders = await orderRepository.GetByUserIdAsync(userId, cancellationToken);
        return orders?.Any() == true ?
            orders.Select(order => order.ToDto()) :
            Enumerable.Empty<OrderDto>();
    }
    
    private async Task AddTransactionToOrderAsync(Guid orderId, Guid userId, decimal amount, CancellationToken cancellationToken = default)
    {
        var createTransaction = new AddTransactionToOrderModel
        {
            OrderId = orderId,
            UserId = userId,
            Amount = amount,
            TransactionType = TransactionType.Test,
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
        var productSize = await productSizeRepository.GetProductSizeByIdAsync(productSizeId, cancellationToken);
        if (productSize == null) throw new ArgumentNullException(); //TODO

        var price = productSize.Product?.Price;
        if (!price.HasValue) throw new ArgumentNullException(); // TODO
        
        return price.Value;
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