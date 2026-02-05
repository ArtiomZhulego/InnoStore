using Application.Abstractions.TransactionAggregate;
using Microsoft.AspNetCore.Mvc;
using Presentation.Constants;
using Presentation.Models.ErrorModels;

namespace Presentation.Controllers;

[ApiController]
[Route(PathConstants.Transactions.Controller)]
public sealed class TransactionController(
    ITransactionService transactionService
) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(TransactionDTO[]), 201)]
    [ProducesResponseType(typeof(ErrorDetails), 404)]
    [ProducesResponseType(typeof(ErrorDetails), 400)]
    [ProducesResponseType(typeof(ErrorDetails), 500)]
    public async Task<IActionResult> GetTransactionsAsync([FromQuery] TransactionSearchFilter filter, CancellationToken cancellationToken)
    {
        var transactions = await transactionService.GetTransactionsAsync(filter, cancellationToken);
        return Ok(transactions);
    }
} 
