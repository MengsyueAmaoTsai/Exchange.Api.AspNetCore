using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using RichillCapital.Domain.Trading;
using RichillCapital.SharedKernel.EntityFrameworkCore;

namespace RichillCapital.Persistence.Configurations;

internal sealed class AccountBalanceConfiguration : IEntityTypeConfiguration<AccountBalance>
{
    public void Configure(EntityTypeBuilder<AccountBalance> builder)
    {
        builder
            .ToTable("account_balances")
            .HasKey(balance => new
            {
                balance.Currency,
                balance.AccountId,
            });

        builder
            .Property(balance => balance.Currency)
            .HasColumnName("currency")
            .HasEnumerationValueConversion()
            .IsRequired();

        builder
            .Property(balance => balance.Amount)
            .HasColumnName("balance")
            .HasColumnType("decimal(18, 6)")
            .IsRequired();

        builder
            .Property(balance => balance.AccountId)
            .HasColumnName("account_id")
            .HasConversion(
                accountId => accountId.Value,
                value => AccountId.From(value).Value)
            .IsRequired();

        builder
            .HasOne<Account>()
            .WithMany()
            .HasForeignKey("account_id")
            .OnDelete(DeleteBehavior.Cascade);
    }
}