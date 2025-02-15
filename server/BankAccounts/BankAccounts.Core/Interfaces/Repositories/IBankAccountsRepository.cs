using BankAccounts.Domain.Entities;

namespace BankAccounts.Domain.Interfaces.Repositories;

public interface IBankAccountsRepository
{
    Task<BankAccount?> GetByIdAsync(int id);
    Task<BankAccount?> GetByNumberAsync(string accountNumber);
    Task<IEnumerable<BankAccount>> GetByBranchAsync(string branch);
    Task AddAsync(BankAccount bankAccount);
    Task<bool> UpdateEmailAsync(BankAccount bankAccount);
    Task<bool> DeleteAsync(int id);
}
