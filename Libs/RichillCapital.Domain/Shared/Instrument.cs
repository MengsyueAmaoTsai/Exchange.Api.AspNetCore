
using RichillCapital.Domain.Shared;
using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monads;

public sealed class Instrument : Entity<Symbol>
{
    private Instrument(
        Symbol id,
        NonEmptyDescription description)
        : base(id)
    {
        Description = description;
    }

    public Symbol Symbol => Id;

    public NonEmptyDescription Description { get; private set; }

    public static ErrorOr<Instrument> Create(
        Symbol symbol,
        NonEmptyDescription description)
    {
        return new Instrument(symbol, description)
            .ToErrorOr();
    }
}