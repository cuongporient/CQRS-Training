using MediatR;
using Microsoft.AspNetCore.Mvc;
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
    // Body: { "owner": "Bob", "initialBalance": 500000 }
    // Returns: 201 Created with the new account ID
    [HttpPost]
    public async Task<IActionResult> CreateAccount([FromBody] CreateAccountRequest request)
    {
        // TODO: Create a CreateAccountCommand from the request, send it via _mediator
        // Return CreatedAtAction with the new account ID
        throw new NotImplementedException();
    }

    // POST /accounts/{id}/deposit
    // Body: { "amount": 200000 }
    // Returns: 200 OK with the new balance
    [HttpPost("{id:guid}/deposit")]
    public async Task<IActionResult> Deposit(Guid id, [FromBody] AmountRequest request)
    {
        // TODO: Create a DepositCommand from id + request.Amount, send it via _mediator
        // Return Ok(new { balance = newBalance })
        throw new NotImplementedException();
    }

    // POST /accounts/{id}/withdraw
    // Body: { "amount": 100000 }
    // Returns: 200 OK with the new balance, or 400 BadRequest for insufficient funds
    [HttpPost("{id:guid}/withdraw")]
    public async Task<IActionResult> Withdraw(Guid id, [FromBody] AmountRequest request)
    {
        // TODO: Create a WithdrawCommand from id + request.Amount, send it via _mediator
        // Catch InvalidOperationException and return BadRequest with the message
        // Return Ok(new { balance = newBalance }) on success
        throw new NotImplementedException();
    }

    // GET /accounts/{id}/transactions
    // Returns: 200 OK with list of transactions (most recent first)
    [HttpGet("{id:guid}/transactions")]
    public async Task<IActionResult> GetTransactions(Guid id)
    {
        // TODO: Create a GetTransactionHistoryQuery with the account id, send it via _mediator
        // Return Ok(transactions)
        throw new NotImplementedException();
    }
}
