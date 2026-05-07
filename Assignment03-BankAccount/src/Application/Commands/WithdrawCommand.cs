using Assignment03_BankAccount.Application.Interfaces;
using Assignment03_BankAccount.Domain;
using Mediator;

namespace Assignment03_BankAccount.Application.Commands;

public record WithdrawCommand(Guid AccountId, decimal Amount) : IRequest<decimal>;

public class WithdrawCommandHandler : IRequestHandler<WithdrawCommand, decimal>
{
    private readonly IBankRepository _repository;

    public WithdrawCommandHandler(IBankRepository repository)
    {
        _repository = repository;
    }

    public ValueTask<decimal> Handle(WithdrawCommand request, CancellationToken cancellationToken)
    {
        var account = _repository.GetById(request.AccountId)
            ?? throw new InvalidOperationException($"Account {request.AccountId} not found.");

        if (account.Balance < request.Amount)
            throw new InvalidOperationException("Insufficient balance.");

        account.Balance -= request.Amount;

        _repository.AddLog(new TransactionLog
        {
            AccountId = account.Id,
            Type = "Withdrawal",
            Amount = request.Amount,
            BalanceAfter = account.Balance,
            CreatedAt = DateTime.UtcNow
        });

        return ValueTask.FromResult(account.Balance);
    }
}
