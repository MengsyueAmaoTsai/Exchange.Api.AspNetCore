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

    public static ErrorOr<Symbol> From(string symbol)
    {
        if (string.IsNullOrWhiteSpace(symbol))
        {
            return Error
                .Invalid("Symbol cannot be empty.")
                .ToErrorOr<Symbol>();
        }

        if (symbol.Length > MaxLength)
        {
            return Error
                .Invalid($"Symbol cannot be longer than {MaxLength} characters.")
                .ToErrorOr<Symbol>();
        }

        return new Symbol(symbol).ToErrorOr();
    }
}