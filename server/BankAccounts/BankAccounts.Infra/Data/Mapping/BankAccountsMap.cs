using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using BankAccounts.Domain.Entities;

namespace BankAccounts.Infra.Data.Mapping;
public class BankAccountMap : IEntityTypeConfiguration<BankAccount>
{
    public void Configure(EntityTypeBuilder<BankAccount> builder)
    {
        builder.ToTable("BankAccounts", "bankaccounts");

        builder.HasKey(b => b.Id);

        builder.HasIndex(b => b.Number).IsUnique();

        builder.Property(b => b.Branch)
            .IsRequired()
            .HasMaxLength(5);

        builder.Property(b => b.Number)
            .IsRequired()
            .HasMaxLength(10);

        builder.Property(b => b.HolderName)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(b => b.HolderEmail)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(b => b.HolderDocument)
            .IsRequired()
            .HasMaxLength(14);

        builder.Property(b => b.Type)
            .IsRequired()
            .HasConversion<string>();

        builder.Property(b => b.HolderType)
            .IsRequired()
            .HasConversion<string>();

        builder.Property(b => b.Status)
            .IsRequired()
            .HasConversion<string>();

        builder.Property(b => b.CreatedAt)
            .IsRequired();

        builder.Property(b => b.UpdatedAt);

    }
}
