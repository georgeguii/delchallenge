using BankAccounts.Enums;

namespace BankAccounts.Application.UseCases.CreateBankAccount;

public class CreateBankAccountResponse
{
    public int Id { get; set; }
    public string Branch { get; set; }
    public string Number { get; set; }
    public AccountType Type { get; set; }
    public string HolderName { get; set; }
    public string HolderEmail { get; set; }
    public string HolderDocument { get; set; }
    public HolderType HolderType { get; set; }
    public AccountStatus Status { get; set; }
}
