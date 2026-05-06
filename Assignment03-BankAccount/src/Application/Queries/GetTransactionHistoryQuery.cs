using MediatR;
using Assignment03_BankAccount.Application.Interfaces;

namespace Assignment03_BankAccount.Application.Queries;

public record TransactionDto(string Type, decimal Amount, decimal BalanceAfter, DateTime CreatedAt);

public record GetTransactionHistoryQuery(Guid AccountId) : IRequest<List<TransactionDto>>;

public class GetTransactionHistoryQueryHandler : IRequestHandler<GetTransactionHistoryQuery, List<TransactionDto>>
{
    private readonly IBankRepository _bankRepository;

    public GetTransactionHistoryQueryHandler(IBankRepository bankRepository)
    {
        _bankRepository = bankRepository;
    }

    public Task<List<TransactionDto>> Handle(GetTransactionHistoryQuery request, CancellationToken cancellationToken)
    {
        // TODO 1: Fetch logs for AccountId from repository (already sorted descending)
        // TODO 2: Map each log to TransactionDto, return list
        throw new NotImplementedException();
    }
}
