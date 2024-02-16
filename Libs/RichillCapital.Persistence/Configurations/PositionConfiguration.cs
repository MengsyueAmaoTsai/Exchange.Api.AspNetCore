using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using RichillCapital.Domain.Trading;
using RichillCapital.SharedKernel.EntityFrameworkCore;

namespace RichillCapital.Persistence.Configurations;

internal sealed class PositionConfiguration : IEntityTypeConfiguration<Position>
{
    public void Configure(EntityTypeBuilder<Position> builder)
    {
        builder
            .ToTable("positions")
            .HasKey(position => position.Id);

        builder
            .Property(position => position.Id)
            .HasColumnName("id")
            .HasMaxLength(PositionId.MaxLength)
            .HasConversion(
                positionId => positionId.Value,
                value => PositionId.From(value).Value)
            .IsRequired();

        builder
            .Property(position => position.Side)
            .HasColumnName("side")
            .HasEnumerationValueConversion()
            .IsRequired();

        builder
            .Property(position => position.Symbol)
            .HasColumnName("symbol")
            .HasConversion(
                symbol => symbol.Value,
                value => Symbol.From(value).Value)
            .IsRequired();

        builder
            .Property(position => position.Quantity)
            .HasColumnName("quantity")
            .HasColumnType("decimal(18, 8)")
            .IsRequired();

        builder
            .Property(position => position.Price)
            .HasColumnName("price")
            .HasColumnType("decimal(18, 8)")
            .IsRequired();

        builder
            .Property(position => position.Commission)
            .HasColumnName("commission")
            .HasColumnType("decimal(18, 8)")
            .IsRequired();

        builder
            .Property(position => position.Tax)
            .HasColumnName("tax")
            .HasColumnType("decimal(18, 8)")
            .IsRequired();

        builder
            .Property(position => position.Swap)
            .HasColumnName("swap")
            .HasColumnType("decimal(18, 8)")
            .IsRequired();

        builder
            .Property(position => position.AccountId)
            .HasColumnName("account_id")
            .HasMaxLength(AccountId.MaxLength)
            .HasConversion(
                accountId => accountId.Value,
                value => AccountId.From(value).Value)
            .IsRequired();

        builder
            .HasOne<Account>()
            .WithMany(account => account.Positions)
            .HasForeignKey("account_id")
            .OnDelete(DeleteBehavior.Cascade);
    }
}