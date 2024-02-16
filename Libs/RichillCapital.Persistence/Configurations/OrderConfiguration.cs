using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using RichillCapital.Domain.Trading;
using RichillCapital.SharedKernel.EntityFrameworkCore;

namespace RichillCapital.Persistence.Configurations;

internal sealed class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder
            .ToTable("orders")
            .HasKey(order => order.Id);

        builder
            .Property(order => order.Id)
            .HasColumnName("id")
            .HasMaxLength(OrderId.MaxLength)
            .HasConversion(
                orderId => orderId.Value,
                value => OrderId.From(value).Value)
            .IsRequired();

        builder
            .Property(order => order.TradeType)
            .HasColumnName("trade_type")
            .HasEnumerationValueConversion()
            .IsRequired();

        builder
            .Property(order => order.Quantity)
            .HasColumnName("quantity")
            .HasColumnType("decimal(18, 8)")
            .IsRequired();

        builder
            .Property(order => order.Symbol)
            .HasColumnName("symbol")
            .HasConversion(
                symbol => symbol.Value,
                value => Symbol.From(value).Value)
            .IsRequired();

        builder
            .Property(order => order.Type)
            .HasColumnName("type")
            .HasEnumerationValueConversion()
            .IsRequired();

        builder
            .Property(order => order.TimeInForce)
            .HasColumnName("time_in_force")
            .HasEnumerationValueConversion()
            .IsRequired();

        builder
            .Property(order => order.Status)
            .HasColumnName("status")
            .HasEnumerationValueConversion()
            .IsRequired();

        builder
            .Property(order => order.AccountId)
            .HasColumnName("account_id")
            .HasMaxLength(AccountId.MaxLength)
            .HasConversion(
                accountId => accountId.Value,
                value => AccountId.From(value).Value)
            .IsRequired();

        builder
            .HasOne<Account>()
            .WithMany(account => account.Orders)
            .HasForeignKey("account_id")
            .OnDelete(DeleteBehavior.Cascade);
    }
}