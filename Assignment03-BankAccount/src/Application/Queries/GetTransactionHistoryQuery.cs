using Assignment03_BankAccount.Application.Interfaces;
using Mediator;

namespace Assignment03_BankAccount.Application.Queries;

public record TransactionDto(string Type, decimal Amount, decimal BalanceAfter, DateTime CreatedAt);

public record GetTransactionHistoryQuery(Guid AccountId) : IRequest<List<TransactionDto>>;

public class GetTransactionHistoryQueryHandler : IRequestHandler<GetTransactionHistoryQuery, List<TransactionDto>>
{
    private readonly IBankRepository _repository;

    public GetTransactionHistoryQueryHandler(IBankRepository repository)
    {
        _repository = repository;
    }

    public ValueTask<List<TransactionDto>> Handle(GetTransactionHistoryQuery request, CancellationToken cancellationToken)
    {
        var logs = _repository.GetLogs(request.AccountId);

        var result = logs
            .Select(l => new TransactionDto(l.Type, l.Amount, l.BalanceAfter, l.CreatedAt))
            .ToList();

        return ValueTask.FromResult(result);
    }
}
