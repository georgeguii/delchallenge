using BankAccounts.Domain.Entities;
using BankAccounts.Domain.Interfaces.Repositories;
using BankAccounts.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace BankAccounts.Infra.Repositories;

public class BankAccountsRepository(BankAccountsContext context) : IBankAccountsRepository
{

    public async Task<BankAccount?> GetByIdAsync(int id)
    {
        return await context.BankAccounts
            .Include(b => b.Balance)
            .FirstOrDefaultAsync(b => b.Id == id);
    }

    public async Task<BankAccount?> GetByNumberAsync(string accountNumber)
    {
        return await context.BankAccounts
            .Include(b => b.Balance)
            .FirstOrDefaultAsync(b => b.Number == accountNumber);
    }

    public async Task<IEnumerable<BankAccount>> GetByBranchAsync(string branch)
    {
        return await context.BankAccounts
            .Where(b => b.Branch == branch)
            .ToListAsync();
    }

    public async Task AddAsync(BankAccount bankAccount)
    {
        await context.BankAccounts.AddAsync(bankAccount);
    }

    public async Task<bool> UpdateEmailAsync(BankAccount bankAccount)
    {
        var updated = await context.BankAccounts
            .Where(x => x.Id == bankAccount.Id)
            .ExecuteUpdateAsync(x => x.SetProperty(x => x.HolderEmail, bankAccount.HolderEmail));

        return updated != 0;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var result = await context.BankAccounts.Where(x => x.Id.Equals(id)).ExecuteDeleteAsync();
        return result != 0;
    }
}
