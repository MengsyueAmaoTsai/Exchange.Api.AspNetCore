using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Domain.Trading;

public sealed class Symbol : SingleValueObject<string>
{
    public const int MaxLength = 100;

    private Symbol(string value) :
        base(value)
    {
    }

    public static Result<Symbol> From(string symbol) => throw new NotImplementedException();
    // Result<string>
    //     .Ensure(symbol, SymbolRules.Values)
    //     .Then(symbol => new Symbol(symbol));

    private static bool NotLongerThanMaxLength(string symbol) =>
        symbol.Length <= MaxLength;

    private static bool NotEmpty(string symbol) =>
        !string.IsNullOrWhiteSpace(symbol);
}

internal static class SymbolRules
{
    public static (Func<string, bool> ensure, Error error)[] Values = [
        (value => !string.IsNullOrWhiteSpace(value), SymbolErrors.Empty),
        (value => value.Length <= Symbol.MaxLength, SymbolErrors.MaxLengthExceeded),
    ];
}

internal static class SymbolErrors
{
    public static readonly Error Empty = Error.Invalid("Symbol cannot be empty.");

    public static readonly Error MaxLengthExceeded = Error
        .Invalid($"Symbol cannot be longer than {Symbol.MaxLength} characters.");
}
