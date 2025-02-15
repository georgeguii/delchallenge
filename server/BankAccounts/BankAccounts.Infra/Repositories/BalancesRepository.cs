using BankAccounts.Domain.Entities;
using BankAccounts.Domain.Interfaces.Repositories;
using BankAccounts.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace BankAccounts.Infra.Repositories;

public class BalancesRepository(BankAccountsContext context) : IBalancesRepository
{

    public async Task<Balance?> GetByBankAccountIdAsync(int bankAccountId)
    {
        return await context.Balances
            .Where(b => b.BankAccountId == bankAccountId)
            .FirstOrDefaultAsync();
    }

    public async Task AddAsync(Balance balance)
    {
        await context.Balances.AddAsync(balance);
    }

    public async Task<bool> UpdateAmountsAsync(Balance balance)
    {
        var updated = await context
            .Balances
            .Where(x => x.BankAccountId == balance.BankAccountId)
            .ExecuteUpdateAsync(x =>
                x.SetProperty(x => x.AvailableAmount, balance.AvailableAmount)
                .SetProperty(x => x.BlockedAmount, balance.BlockedAmount));

        return updated != 0;
    }

    public async Task DeleteAsync(int bankAccountId)
    {
        var balance = await GetByBankAccountIdAsync(bankAccountId);
        if (balance != null)
        {
            context.Balances.Remove(balance);
        }
    }
}
