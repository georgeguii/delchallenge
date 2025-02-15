namespace BankAccounts.Domain.Entities;

public class Balance
{
    public int BankAccountId { get; set; }
    public virtual BankAccount? BankAccount { get; set; }


    public decimal AvailableAmount { get; set; }
    public decimal BlockedAmount { get; set; }

}