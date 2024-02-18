using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RichillCapital.SharedKernel.EntityFrameworkCore;

public static class EnumerationTypePropertyBuilderExtensions
{
    public static PropertyBuilder<TProperty> HasEnumerationValueConversion<TProperty>(
        this PropertyBuilder<TProperty> builder)
        where TProperty : Enumeration<TProperty> =>
        builder.HasConversion(
            enumeration => enumeration.Value,
            value => Enumeration<TProperty>.FromValue(value).Value);

    public static PropertyBuilder<TProperty> HasEnumerationNameConversion<TProperty>(
        this PropertyBuilder<TProperty> builder, bool ignoreCase = false)
        where TProperty : Enumeration<TProperty> =>
        builder
            .HasMaxLength(Enumeration<TProperty>.Members
                .Max(member => member.Name.Length))
            .HasConversion(
                enumeration => enumeration.Name,
                name => Enumeration<TProperty>.FromName(name, ignoreCase).Value);
}