namespace Assignment03_BankAccount.Domain;

public class BankAccount
{
    public Guid Id { get; set; }
    public string Owner { get; set; } = string.Empty;
    public decimal Balance { get; set; }
}
