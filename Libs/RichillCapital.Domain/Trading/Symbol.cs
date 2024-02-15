using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monad;

namespace RichillCapital.Domain.Trading;

public sealed class Symbol : SingleValueObject<string>
{
    public const int MaxLength = 100;

    private Symbol(string value) :
        base(value)
    {
    }

    public static ErrorOr<Symbol> From(string symbol)
    {
        if (string.IsNullOrWhiteSpace(symbol))
        {
            return Error.Invalid("Symbol cannot be empty.");
        }

        if (symbol.Length > MaxLength)
        {
            return Error.Invalid($"Symbol cannot be longer than {MaxLength} characters.");
        }

        return new Symbol(symbol);
    }
}