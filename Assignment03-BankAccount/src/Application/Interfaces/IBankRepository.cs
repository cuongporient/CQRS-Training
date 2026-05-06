using Assignment03_BankAccount.Domain;

namespace Assignment03_BankAccount.Application.Interfaces;

public interface IBankRepository
{
    void AddAccount(BankAccount account);
    BankAccount? GetById(Guid id);
    void AddLog(TransactionLog log);
    List<TransactionLog> GetLogs(Guid accountId);
}
