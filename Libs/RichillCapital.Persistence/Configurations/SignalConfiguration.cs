using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using RichillCapital.Domain.Bots;
using RichillCapital.Domain.Shared;
using RichillCapital.Domain.Trading;
using RichillCapital.SharedKernel.EntityFrameworkCore;

namespace RichillCapital.Persistence.Configurations;

internal sealed class TradingSignalConfiguration :
    IEntityTypeConfiguration<Signal>
{
    public void Configure(EntityTypeBuilder<Signal> builder)
    {
        builder
            .ToTable("trading_signals")
            .HasKey(signal => new
            {
                signal.Time,
                signal.TradeType,
                signal.Symbol,
                signal.Quantity,
                signal.Price,
                signal.BotId,
            });

        builder
            .Property(signal => signal.Time)
            .HasColumnName("time")
            .HasColumnType("timestamp with time zone")
            .IsRequired();

        builder
            .Property(signal => signal.TradeType)
            .HasColumnName("trade_type")
            .HasEnumerationValueConversion()
            .IsRequired();

        builder
            .Property(signal => signal.Symbol)
            .HasColumnName("symbol")
            .HasConversion(
                symbol => symbol.Value,
                value => Symbol.From(value).Value)
            .IsRequired();

        builder
            .Property(signal => signal.Quantity)
            .HasColumnName("quantity")
            .HasColumnType("decimal(18, 8)")
            .IsRequired();

        builder
            .Property(signal => signal.Price)
            .HasColumnName("price")
            .HasColumnType("decimal(18, 8)")
            .IsRequired();

        builder
            .Property(signal => signal.BotId)
            .HasColumnName("bot_id")
            .HasConversion(
                botId => botId.Value,
                value => BotId.From(value).Value)
            .IsRequired();

        builder
            .HasOne<Bot>()
            .WithMany(bot => bot.Signals)
            .HasForeignKey("bot_id")
            .OnDelete(DeleteBehavior.Cascade);
    }
}