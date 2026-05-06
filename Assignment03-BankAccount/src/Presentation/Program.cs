using Microsoft.Extensions.DependencyInjection;
using MediatR;
using Assignment03_BankAccount.Domain;
using Assignment03_BankAccount.Application;
using Assignment03_BankAccount.Application.Interfaces;
using Assignment03_BankAccount.Infrastructure;
using Assignment03_BankAccount.Application.Commands;
using Assignment03_BankAccount.Application.Queries;

// Setup DI
var services = new ServiceCollection();
services.AddApplication();
services.AddInfrastructure();

var serviceProvider = services.BuildServiceProvider();
var mediator = serviceProvider.GetRequiredService<IMediator>();
var bankRepository = serviceProvider.GetRequiredService<IBankRepository>();

try
{
    // Seed account directly via repository (bypass CQRS intentionally)
    var account = new BankAccount { Id = Guid.NewGuid(), Owner = "Bob", Balance = 500_000 };
    bankRepository.AddAccount(account);
    Console.WriteLine($"[OK] Account seeded: Bob, Balance = 500000");

    // Test 1: DepositCommand
    var balanceAfterDeposit = await mediator.Send(new DepositCommand(account.Id, 200_000));
    if (balanceAfterDeposit == 700_000)
    {
        Console.WriteLine($"[OK] DepositCommand succeeded: new balance = {balanceAfterDeposit}");
    }

    // Test 2: WithdrawCommand
    var balanceAfterWithdraw = await mediator.Send(new WithdrawCommand(account.Id, 100_000));
    if (balanceAfterWithdraw == 600_000)
    {
        Console.WriteLine($"[OK] WithdrawCommand succeeded: new balance = {balanceAfterWithdraw}");
    }

    // Test 3: WithdrawCommand with insufficient balance
    try
    {
        await mediator.Send(new WithdrawCommand(account.Id, 999_000));
        Console.WriteLine("[FAIL] Expected InvalidOperationException for insufficient balance");
    }
    catch (InvalidOperationException ex)
    {
        Console.WriteLine($"[OK] WithdrawCommand correctly threw InvalidOperationException: {ex.Message}");
    }

    // Test 4: GetTransactionHistoryQuery
    var history = await mediator.Send(new GetTransactionHistoryQuery(account.Id));
    if (history.Count == 2)
    {
        Console.WriteLine($"[OK] GetTransactionHistoryQuery returned {history.Count} records");
        foreach (var tx in history)
        {
            Console.WriteLine($"  - {tx.Type}: {tx.Amount}, Balance after: {tx.BalanceAfter}");
        }
    }
    else
    {
        Console.WriteLine($"[FAIL] Expected 2 transaction records, got {history.Count}");
    }
}
catch (Exception ex)
{
    Console.WriteLine($"[ERROR] Unexpected exception: {ex.Message}");
}
