using Application.Abstractions.ProductAggregate;
using Application.Abstractions.UserAggregate;
using Microsoft.AspNetCore.Mvc;
using Presentation.Constants;
using Presentation.Models.ErrorModels;

namespace Presentation.Controllers;

[ApiController]
[Route(PathConstants.User.Controller)]
public sealed class UserController(IUserService userService) : ControllerBase
{
    [HttpGet(PathConstants.User.GetCurrentScoresAmount)]
    [ProducesResponseType(typeof(decimal), 201)]
    [ProducesResponseType(typeof(ErrorDetails), 404)]
    [ProducesResponseType(typeof(ErrorDetails), 400)]
    [ProducesResponseType(typeof(ErrorDetails), 500)]
    public async Task<IActionResult> GetUserCurrentScoresAmountAsync([FromQuery] Guid userId, CancellationToken cancellationToken)
    {
        var scoresAmount = await userService.GetCurrentScoresAmountAsync(userId, cancellationToken);
        return Ok(scoresAmount);
    }
}
