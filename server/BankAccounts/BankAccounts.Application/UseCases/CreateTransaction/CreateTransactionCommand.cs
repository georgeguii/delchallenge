using BankAccounts.Enums;

namespace BankAccounts.Application.UseCases.CreateTransaction;
public class CreateTransactionCommand
{
    public TransactionType Type { get; set; }
    public decimal Amount { get; set; }
    public int BankAccountId { get; set; }

    public string CounterpartyBankCode { get; set; }
    public string CounterpartyBankName { get; set; }
    public string CounterpartyBranch { get; set; }
    public string CounterpartyAccountNumber { get; set; }
    public AccountType CounterpartyAccountType { get; set; }
    public string CounterpartyHolderName { get; set; }
    public HolderType CounterpartyHolderType { get; set; }
    public string CounterpartyHolderDocument { get; set; }
}
