namespace Assignment03_BankAccount.Presentation.Models;

public record CreateAccountRequest(string Owner, decimal InitialBalance);

public record AmountRequest(decimal Amount);
