using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using RichillCapital.Domain.Trading;
using RichillCapital.SharedKernel.EntityFrameworkCore;

namespace RichillCapital.Persistence.Configurations;

internal sealed class ExecutionConfiguration : IEntityTypeConfiguration<Execution>
{
    public void Configure(EntityTypeBuilder<Execution> builder)
    {
        builder
            .ToTable("executions")
            .HasKey(execution => new
            {
                execution.Time,
                execution.TradeType,
                execution.Symbol,
                execution.OrderId,
                execution.AccountId,
            });

        builder
            .Property(execution => execution.Time)
            .HasColumnName("time")
            .HasColumnType("timestamp with time zone")
            .IsRequired();

        builder
            .Property(execution => execution.TradeType)
            .HasColumnName("trade_type")
            .HasEnumerationValueConversion()
            .IsRequired();

        builder
            .Property(execution => execution.Symbol)
            .HasColumnName("symbol")
            .HasConversion(
                symbol => symbol.Value,
                value => Symbol.From(value).Value)
            .IsRequired();

        builder
            .Property(execution => execution.Quantity)
            .HasColumnName("quantity")
            .HasColumnType("decimal(18, 8)")
            .IsRequired();

        builder
            .Property(execution => execution.Price)
            .HasColumnName("price")
            .HasColumnType("decimal(18, 8)")
            .IsRequired();

        builder
            .Property(execution => execution.Commission)
            .HasColumnName("commission")
            .HasColumnType("decimal(18, 8)")
            .IsRequired();

        builder
            .Property(execution => execution.Tax)
            .HasColumnName("tax")
            .HasColumnType("decimal(18, 8)")
            .IsRequired();

        builder
            .Property(execution => execution.AccountId)
            .HasColumnName("account_id")
            .HasMaxLength(AccountId.MaxLength)
            .HasConversion(
                accountId => accountId.Value,
                value => AccountId.From(value).Value)
            .IsRequired();

        builder
            .Property(execution => execution.OrderId)
            .HasColumnName("order_id")
            .HasMaxLength(OrderId.MaxLength)
            .HasConversion(
                orderId => orderId.Value,
                value => OrderId.From(value).Value)
            .IsRequired();

        builder
            .HasOne<Account>()
            .WithMany()
            .HasForeignKey("account_id")
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne<Order>()
            .WithMany()
            .HasForeignKey("order_id")
            .OnDelete(DeleteBehavior.Cascade);
    }
}
