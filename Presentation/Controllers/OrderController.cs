using Application.Abstractions.DTOs.Entities;
using Application.Abstractions.OrderAggregate;
using Application.Abstractions.OrderAggregate.Models;
using Microsoft.AspNetCore.Mvc;
using Presentation.Constants;
using Presentation.Models.ErrorModels;

namespace Presentation.Controllers;

[ApiController]
[Route(PathConstants.Orders.Controller)]
public sealed class OrderController(IOrderService orderService) : ControllerBase
{
    [HttpPost(PathConstants.Orders.Create)]
    [ProducesResponseType(typeof(OrderDto), 201)]
    [ProducesResponseType(typeof(ErrorDetails), 404)]
    [ProducesResponseType(typeof(ErrorDetails), 400)]
    [ProducesResponseType(typeof(ErrorDetails), 500)]
    public async Task<IActionResult> CreateOrder([FromBody] CreateOrderModel createProductModel, CancellationToken cancellationToken)
    {
        var order = await orderService.CreateOrderAsync(createProductModel, cancellationToken);
        return CreatedAtAction(nameof(GetOrderById), new { id = order.Id, }, order);
    }
    
    [HttpPost(PathConstants.Orders.Cancel)]
    [ProducesResponseType(typeof(OrderDto), 201)]
    [ProducesResponseType(typeof(ErrorDetails), 404)]
    [ProducesResponseType(typeof(ErrorDetails), 400)]
    [ProducesResponseType(typeof(ErrorDetails), 500)]
    public async Task<IActionResult> CancelOrder([FromBody] CancelOrderModel cancelOrderModel, CancellationToken cancellationToken)
    {
        var order = await orderService.CancelOrderAsync(cancelOrderModel, cancellationToken);
        return CreatedAtAction(nameof(GetOrderById), new { id = order?.Id, }, order);
    }
    

    [HttpGet(PathConstants.Orders.GetById)]
    [ProducesResponseType(typeof(OrderDto), 200)]
    [ProducesResponseType(typeof(ErrorDetails), 404)]
    [ProducesResponseType(typeof(ErrorDetails), 500)]
    public async Task<IActionResult> GetOrderById(Guid id, CancellationToken cancellationToken)
    {
        var order = await orderService.GetOrderByIdAsync(id, cancellationToken);
        return Ok(order);
    }
    
    [HttpGet(PathConstants.Orders.GetByUserId)]
    [ProducesResponseType(typeof(IEnumerable<OrderDto>), 200)]
    [ProducesResponseType(typeof(ErrorDetails), 404)]
    [ProducesResponseType(typeof(ErrorDetails), 500)]
    public async Task<IActionResult> GetOrderByUserId(Guid id, CancellationToken cancellationToken)
    {
        var orders = await orderService.GetOrdersByUserAsync(id, cancellationToken);
        return Ok(orders);
    }
}