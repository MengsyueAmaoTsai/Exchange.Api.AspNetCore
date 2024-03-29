using RichillCapital.SharedKernel;

namespace RichillCapital.Domain.Bots;

public static class BotErrors
{
    public static Error NotFound(BotId id) =>
        Error.NotFound($"Bot with id '{id}' not found.");

    public static Error Duplicate(BotId id) =>
        Error.Conflict($"Bot with the same id already exists. Id: '{id}'.");

    public static Error Duplicate(BotName name) =>
        Error.Conflict($"Bot with the same name already exists. Name: '{name}'.");

    public static Error TradingPlatformNotSupported(TradingPlatform platform) =>
        Error.Invalid($"Trading platform '{platform}' is not supported.");
}