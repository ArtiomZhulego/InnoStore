using Application.Abstractions.DTOs.Entities;
using Application.Abstractions.OrderAggregate;
using Microsoft.AspNetCore.Mvc;
using Presentation.Constants;
using Presentation.Models.ErrorModels;

namespace Presentation.Controllers
{
    [ApiController]
    [Route(PathConstants.Users.Controller)]
    internal class UserController : ControllerBase
    {
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
}