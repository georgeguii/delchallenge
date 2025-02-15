using BankAccounts.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace BankAccounts.Infra.Data.Mapping;
public class BalancesMap : IEntityTypeConfiguration<Balance>
{
    public void Configure(EntityTypeBuilder<Balance> builder)
    {
        builder.ToTable("Balances");

        builder.HasKey(b => b.BankAccountId);

        builder.HasOne(b => b.BankAccount)
            .WithOne(b => b.Balance)
            .HasForeignKey<Balance>(b => b.BankAccountId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(b => b.AvailableAmount)
            .IsRequired()
            .HasPrecision(18, 2)
            .HasDefaultValue(0);

        builder.Property(b => b.BlockedAmount)
            .IsRequired()
            .HasPrecision(18, 2)
            .HasDefaultValue(0);
    }
}
