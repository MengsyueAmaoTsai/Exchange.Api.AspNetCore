using RichillCapital.Domain.Bots;
using RichillCapital.SharedKernel;

namespace RichillCapital.Domain;

public static class DomainErrors
{
    public static class Bots
    {
        public static Error NotFound(BotId id) =>
            Error.NotFound($"Bot with specified id '{id.Value}' not found.");
    }
}