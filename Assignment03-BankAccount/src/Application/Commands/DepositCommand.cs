using Assignment03_BankAccount.Application.Interfaces;
using Assignment03_BankAccount.Domain;
using Mediator;

namespace Assignment03_BankAccount.Application.Commands;

public record DepositCommand(Guid AccountId, decimal Amount) : IRequest<decimal>;

public class DepositCommandHandler : IRequestHandler<DepositCommand, decimal>
{
    private readonly IBankRepository _repository;

    public DepositCommandHandler(IBankRepository repository)
    {
        _repository = repository;
    }

    public ValueTask<decimal> Handle(DepositCommand request, CancellationToken cancellationToken)
    {
        var account = _repository.GetById(request.AccountId)
            ?? throw new InvalidOperationException($"Account {request.AccountId} not found.");

        account.Balance += request.Amount;

        _repository.AddLog(new TransactionLog
        {
            AccountId = account.Id,
            Type = "Deposit",
            Amount = request.Amount,
            BalanceAfter = account.Balance,
            CreatedAt = DateTime.UtcNow
        });

        return ValueTask.FromResult(account.Balance);
    }
}
