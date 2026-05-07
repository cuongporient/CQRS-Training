using Mediator;
using Microsoft.AspNetCore.Mvc;
using Assignment03_BankAccount.Application.Commands;
using Assignment03_BankAccount.Application.Exceptions;
using Assignment03_BankAccount.Application.Queries;
using Assignment03_BankAccount.Presentation.Models;

namespace Assignment03_BankAccount.Presentation.Controllers;

[ApiController]
[Route("accounts")]
public class AccountsController : ControllerBase
{
    private readonly IMediator _mediator;

    public AccountsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    // POST /accounts
    [HttpPost]
    public async Task<IActionResult> CreateAccount([FromBody] CreateAccountRequest request)
    {
        try
        {
            var id = await _mediator.Send(new CreateAccountCommand(request.Owner, request.InitialBalance));
            return CreatedAtAction(nameof(GetTransactions), new { id }, new { id });
        }
        catch (ValidationException ex)
        {
            return BadRequest(new { errors = ex.Errors });
        }
    }

    // POST /accounts/{id}/deposit
    [HttpPost("{id:guid}/deposit")]
    public async Task<IActionResult> Deposit(Guid id, [FromBody] AmountRequest request)
    {
        try
        {
            var balance = await _mediator.Send(new DepositCommand(id, request.Amount));
            return Ok(new { balance });
        }
        catch (ValidationException ex)
        {
            return BadRequest(new { errors = ex.Errors });
        }
    }

    // POST /accounts/{id}/withdraw
    [HttpPost("{id:guid}/withdraw")]
    public async Task<IActionResult> Withdraw(Guid id, [FromBody] AmountRequest request)
    {
        try
        {
            var balance = await _mediator.Send(new WithdrawCommand(id, request.Amount));
            return Ok(new { balance });
        }
        catch (ValidationException ex)
        {
            return BadRequest(new { errors = ex.Errors });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    // GET /accounts/{id}/transactions
    [HttpGet("{id:guid}/transactions")]
    public async Task<IActionResult> GetTransactions(Guid id)
    {
        var transactions = await _mediator.Send(new GetTransactionHistoryQuery(id));
        return Ok(transactions);
    }
}
