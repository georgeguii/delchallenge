using BankAccounts.Enums;
using MediatR;

namespace BankAccounts.Application.UseCases.CreateBankAccount;
public class CreateBankAccountCommand : IRequest<CreateBankAccountResponse>
{
    public string Branch { get; set; }

    public AccountType AccountType { get; set; }

    public string Name { get; set; }

    public string Email { get; set; }

    public string Document { get; set; }

    public HolderType LegalType { get; set; }
}
