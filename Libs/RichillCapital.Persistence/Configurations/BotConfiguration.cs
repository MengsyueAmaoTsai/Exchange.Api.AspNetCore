using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using RichillCapital.Domain.Bots;
using RichillCapital.Domain.Shared;
using RichillCapital.SharedKernel.EntityFrameworkCore;

namespace RichillCapital.Persistence.Configurations;

internal sealed class BotConfiguration : IEntityTypeConfiguration<Bot>
{
    public void Configure(EntityTypeBuilder<Bot> builder)
    {
        builder
            .ToTable("bots")
            .HasKey(bot => bot.Id);

        builder
            .Property(bot => bot.Id)
            .HasColumnName("id")
            .HasMaxLength(BotId.MaxLength)
            .HasConversion(
                botId => botId.Value,
                value => BotId.From(value).Value)
            .IsRequired();

        builder
            .Property(bot => bot.Name)
            .HasColumnName("name")
            .HasMaxLength(BotName.MaxLength)
            .HasConversion(
                botName => botName.Value,
                value => BotName.From(value).Value)
            .IsRequired();

        builder
            .Property(bot => bot.Description)
            .HasColumnName("description")
            .HasMaxLength(NonEmptyDescription.MaxLength)
            .HasConversion(
                description => description.Value,
                value => NonEmptyDescription.From(value).Value)
            .IsRequired();

        builder
            .Property(bot => bot.Side)
            .HasColumnName("side")
            .HasEnumerationValueConversion()
            .IsRequired();

        builder
            .Property(bot => bot.Platform)
            .HasColumnName("platform")
            .HasEnumerationValueConversion()
            .IsRequired();
    }
}
