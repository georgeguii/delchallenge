using BankAccounts.Domain.Entities;
using BankAccounts.Domain.Interfaces.Repositories;
using MediatR;
using System.Security.Cryptography;

namespace BankAccounts.Application.UseCases.CreateBankAccount;

public class CreateBankAccountHandler : IRequestHandler<CreateBankAccountCommand, CreateBankAccountResponse>
{
    private const long MinValue = 10_000L;
    private const long MaxValue = 9_999_999_999L;
    private const long Range = MaxValue - MinValue + 1;

    private readonly IUnitOfWork _unitOfWork;
    private readonly IBankAccountsRepository _bankAccountRepository;

    public CreateBankAccountHandler(IUnitOfWork unitOfWork, IBankAccountsRepository bankAccountRepository)
    {
        _unitOfWork = unitOfWork;
        _bankAccountRepository = bankAccountRepository;
    }

    public async Task<CreateBankAccountResponse> Handle(CreateBankAccountCommand request, CancellationToken cancellationToken)
    {
        var accountNumber = GenerateAccountNumber();

        var entity = new BankAccount(request.Branch, accountNumber, request.AccountType,
            request.Name, request.Email, request.Document, request.LegalType);

        await _bankAccountRepository.AddAsync(entity);
        await _unitOfWork.Commit(cancellationToken);

        return new CreateBankAccountResponse()
        {
            Id = entity.Id,
            Branch = entity.Branch,
            Number = entity.Number,
            Type = entity.Type,
            HolderName = entity.HolderName,
            HolderEmail = entity.HolderEmail,
            HolderDocument = entity.HolderDocument,
            HolderType = entity.HolderType,
            Status = entity.Status
        };
    }

    private static string GenerateAccountNumber()
    {
        using var rng = RandomNumberGenerator.Create();

        while (true)
        {
            var buffer = new byte[8];
            rng.GetBytes(buffer);

            var randomNumber = BitConverter.ToUInt64(buffer);
            var candidate = (long)(randomNumber % (ulong)Range) + MinValue;

            if (IsValidAccountNumber(candidate))
                return candidate.ToString();
        }
    }

    private static bool IsValidAccountNumber(long number)
    {
        return number is >= MinValue and <= MaxValue;
    }
}
