using BankAccounts.Enums;

namespace BankAccounts.Domain.Entities;

public class BankAccount
{
    public int Id { get; private set; }
    public string Branch { get; private set; }
    public string Number { get; private set; }
    public AccountType Type { get; private set; }
    public string HolderName { get; private set; }
    public string HolderEmail { get; private set; }
    public string HolderDocument { get; private set; }
    public HolderType HolderType { get; private set; }
    public AccountStatus Status { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    public virtual Balance Balance { get; set; }

    public BankAccount(string branch,string number, AccountType type,
        string holderName, string holderEmail, string holderDocument,
        HolderType holderType)
    {
        Branch = branch;
        Number = number;
        Type = type;
        HolderName = holderName;
        HolderEmail = holderEmail;
        HolderDocument = holderDocument;
        HolderType = holderType;
        Status = AccountStatus.ACTIVE;
        CreatedAt = DateTime.Now;
        Balance = new Balance();
    }
}
