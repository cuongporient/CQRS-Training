using MediatR;
using Assignment03_BankAccount.Domain;
using Assignment03_BankAccount.Application.Interfaces;

namespace Assignment03_BankAccount.Application.Commands;

public record DepositCommand(Guid AccountId, decimal Amount) : IRequest<decimal>;

public class DepositCommandHandler : IRequestHandler<DepositCommand, decimal>
{
    private readonly IBankRepository _bankRepository;

    public DepositCommandHandler(IBankRepository bankRepository)
    {
        _bankRepository = bankRepository;
    }

    public Task<decimal> Handle(DepositCommand request, CancellationToken cancellationToken)
    {
        // TODO 1: Find account by AccountId — throw ArgumentException if not found
        // TODO 2: Validate Amount > 0 — throw ArgumentException
        // TODO 3: Add Amount to account Balance
        // TODO 4: Write a TransactionLog (Type="Deposit", BalanceAfter = balance after update)
        // TODO 5: Return new Balance
        throw new NotImplementedException();
    }
}
