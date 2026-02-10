using Application.Abstractions.DTOs.Entities;
using Application.Abstractions.OrderAggregate;
using Application.Abstractions.UserAggregate;
using Microsoft.AspNetCore.Mvc;
using Presentation.Constants;
using Presentation.Models.ErrorModels;

namespace Presentation.Controllers;

[ApiController]
[Route(PathConstants.Users.Controller)]
public sealed class UserController(IUserService userService) : ControllerBase
{
    [HttpGet(PathConstants.Users.GetUserBalance)]
    [ProducesResponseType(typeof(decimal), 201)]
    [ProducesResponseType(typeof(ErrorDetails), 404)]
    [ProducesResponseType(typeof(ErrorDetails), 400)]
    [ProducesResponseType(typeof(ErrorDetails), 500)]
    public async Task<IActionResult> GetUserBalanceAsync([FromQuery] Guid userId, CancellationToken cancellationToken)
    {
        var userBalance = await userService.GetUserBalanceAsync(userId, cancellationToken);
        return Ok(userBalance);
    }

    [HttpGet(PathConstants.Users.GetOrders)]
    [ProducesResponseType(typeof(IEnumerable<OrderDto>), 200)]
    [ProducesResponseType(typeof(ErrorDetails), 404)]
    [ProducesResponseType(typeof(ErrorDetails), 500)]
    public async Task<IActionResult> GetOrderByUserId([FromServices] IOrderService orderService, Guid id, CancellationToken cancellationToken)
    {
        var orders = await orderService.GetOrdersByUserIdAsync(id, cancellationToken);
        return Ok(orders);
    }
}
