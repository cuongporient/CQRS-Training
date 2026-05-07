using Assignment03_BankAccount.Application.Interfaces;
using Assignment03_BankAccount.Domain;
using Mediator;

namespace Assignment03_BankAccount.Application.Commands;

public record CreateAccountCommand(string Owner, decimal InitialBalance) : IRequest<Guid>;

public class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand, Guid>
{
    private readonly IBankRepository _repository;

    public CreateAccountCommandHandler(IBankRepository repository)
    {
        _repository = repository;
    }

    public ValueTask<Guid> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
    {
        var account = new BankAccount
        {
            Id = Guid.NewGuid(),
            Owner = request.Owner,
            Balance = request.InitialBalance
        };

        _repository.AddAccount(account);

        if (request.InitialBalance > 0)
        {
            _repository.AddLog(new TransactionLog
            {
                AccountId = account.Id,
                Type = "Deposit",
                Amount = request.InitialBalance,
                BalanceAfter = account.Balance,
                CreatedAt = DateTime.UtcNow
            });
        }

        return ValueTask.FromResult(account.Id);
    }
}
