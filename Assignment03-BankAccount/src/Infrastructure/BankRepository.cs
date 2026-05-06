using Assignment03_BankAccount.Domain;
using Assignment03_BankAccount.Application.Interfaces;

namespace Assignment03_BankAccount.Infrastructure;

public class BankRepository : IBankRepository
{
    private readonly List<BankAccount> _accounts = new();
    private readonly List<TransactionLog> _transactionLogs = new();

    public void AddAccount(BankAccount account)
    {
        _accounts.Add(account);
    }

    public BankAccount? GetById(Guid id)
    {
        return _accounts.FirstOrDefault(a => a.Id == id);
    }

    public void AddLog(TransactionLog log)
    {
        _transactionLogs.Add(log);
    }

    public List<TransactionLog> GetLogs(Guid accountId)
    {
        return _transactionLogs
            .Where(t => t.AccountId == accountId)
            .OrderByDescending(t => t.CreatedAt)
            .ToList();
    }
}
