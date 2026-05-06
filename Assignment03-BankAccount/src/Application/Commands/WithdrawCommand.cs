using MediatR;
using Assignment03_BankAccount.Application.Interfaces;

namespace Assignment03_BankAccount.Application.Commands;

public record WithdrawCommand(Guid AccountId, decimal Amount) : IRequest<decimal>;

public class WithdrawCommandHandler : IRequestHandler<WithdrawCommand, decimal>
{
    private readonly IBankRepository _bankRepository;

    public WithdrawCommandHandler(IBankRepository bankRepository)
    {
        _bankRepository = bankRepository;
    }

    public Task<decimal> Handle(WithdrawCommand request, CancellationToken cancellationToken)
    {
        // TODO 1: Find account by AccountId — throw ArgumentException if not found
        // TODO 2: Validate Amount > 0 — throw ArgumentException
        // TODO 3: Validate Balance >= Amount — throw InvalidOperationException("Insufficient balance") if not
        // TODO 4: Subtract Amount from Balance
        // TODO 5: Write a TransactionLog (Type="Withdrawal", BalanceAfter = balance after update)
        // TODO 6: Return new Balance
        // ❓ QUESTION: The handler modifies BankAccount balance AND writes a TransactionLog
        //    in two separate steps. If the log write fails after the balance was already updated,
        //    what happens to the data? What mechanism would a real database need?
        throw new NotImplementedException();
    }
}
