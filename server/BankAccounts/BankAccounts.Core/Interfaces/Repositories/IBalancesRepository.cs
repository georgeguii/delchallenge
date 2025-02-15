using BankAccounts.Domain.Entities;

namespace BankAccounts.Domain.Interfaces.Repositories;

public interface IBalancesRepository
{
    Task<Balance?> GetByBankAccountIdAsync(int bankAccountId);
    Task AddAsync(Balance balance);
    Task<bool> UpdateAmountsAsync(Balance balance);
    Task DeleteAsync(int bankAccountId);
}
