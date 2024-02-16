using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using RichillCapital.Domain.Trading;
using RichillCapital.SharedKernel.EntityFrameworkCore;

namespace RichillCapital.Persistence.PostgreSql.Configurations;

internal sealed class TradeConfiguration : IEntityTypeConfiguration<Trade>
{
    public void Configure(EntityTypeBuilder<Trade> builder)
    {
        builder
            .ToTable("trades")
            .HasKey(trade => new
            {
                trade.Side,
                trade.Symbol,
                trade.Quantity,
                trade.EntryTime,
                trade.EntryPrice,
                trade.ExitTime,
                trade.ExitPrice,
                trade.Commission,
                trade.Tax,
                trade.Swap,
                trade.AccountId,
            });

        builder
            .Property(trade => trade.Side)
            .HasColumnName("side")
            .HasEnumerationValueConversion()
            .IsRequired();

        builder
            .Property(trade => trade.Symbol)
            .HasColumnName("symbol")
            .HasMaxLength(Symbol.MaxLength)
            .HasConversion(
                symbol => symbol.Value,
                value => Symbol.From(value).Value)
            .IsRequired();

        builder
            .Property(trade => trade.Quantity)
            .HasColumnName("quantity")
            .HasColumnType("decimal(18, 8)")
            .IsRequired();

        builder
            .Property(trade => trade.EntryTime)
            .HasColumnName("entry_time")
            .HasColumnType("timestamp with time zone")
            .IsRequired();

        builder
            .Property(trade => trade.EntryPrice)
            .HasColumnName("entry_price")
            .HasColumnType("decimal(18, 8)")
            .IsRequired();

        builder
            .Property(trade => trade.ExitTime)
            .HasColumnName("exit_time")
            .HasColumnType("timestamp with time zone")
            .IsRequired();

        builder
            .Property(trade => trade.ExitPrice)
            .HasColumnName("exit_price")
            .HasColumnType("decimal(18, 8)")
            .IsRequired();

        builder
            .Property(trade => trade.Commission)
            .HasColumnName("commission")
            .HasColumnType("decimal(18, 8)")
            .IsRequired();

        builder
            .Property(trade => trade.Tax)
            .HasColumnName("tax")
            .HasColumnType("decimal(18, 8)")
            .IsRequired();

        builder
            .Property(trade => trade.Swap)
            .HasColumnName("swap")
            .HasColumnType("decimal(18, 8)")
            .IsRequired();

        builder
            .Property(trade => trade.AccountId)
            .HasColumnName("account_id")
            .HasMaxLength(AccountId.MaxLength)
            .HasConversion(
                accountId => accountId.Value,
                value => AccountId.From(value).Value)
            .IsRequired();

        builder
            .HasOne<Account>()
            .WithMany(account => account.Trades)
            .HasForeignKey("account_id")
            .OnDelete(DeleteBehavior.Cascade);
    }
}