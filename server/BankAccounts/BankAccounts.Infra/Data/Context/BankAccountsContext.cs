using BankAccounts.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BankAccounts.Infra.Data.Context;

public class BankAccountsContext(DbContextOptions<BankAccountsContext> options) : DbContext(options)
{
    public DbSet<BankAccount> BankAccounts { get; set; }
    public DbSet<Balance> Balances { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BankAccountsContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}
