using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using RichillCapital.Domain.Trading;
using RichillCapital.SharedKernel.EntityFrameworkCore;

namespace RichillCapital.Persistence.Configurations;

internal sealed class AccountConfiguration : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder
            .ToTable("accounts")
            .HasKey(account => account.Id);

        builder
            .Property(account => account.Id)
            .HasColumnName("id")
            .HasMaxLength(AccountId.MaxLength)
            .HasConversion(
                id => id.Value,
                value => AccountId.From(value).Value)
            .IsRequired();

        builder
            .Property(account => account.Name)
            .HasColumnName("name")
            .HasMaxLength(AccountName.MaxLength)
            .HasConversion(
                name => name.Value,
                value => AccountName.From(value).Value)
            .IsRequired();

        builder
            .Property(account => account.PositionMode)
            .HasColumnName("position_mode")
            .HasEnumerationValueConversion()
            .IsRequired();

        builder
            .Property(account => account.Currency)
            .HasColumnName("currency")
            .HasEnumerationValueConversion()
            .IsRequired();
    }
}