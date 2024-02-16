using RichillCapital.Domain.Bots;
using RichillCapital.Domain.Trading;
using RichillCapital.SharedKernel;

namespace RichillCapital.Domain;

public static class DomainErrors
{
    public static class Bots
    {
        public static Error NotFound(BotId id) =>
            Error.NotFound($"Bot with specified id '{id.Value}' not found.");

        public static Error AlreadyExists(BotId id) =>
            Error.Conflict($"Bot with specified id '{id.Value}' already exists.");
    }

    public static class Accounts
    {
        public static Error NotFound(AccountId id) =>
            Error.NotFound($"Account with specified id '{id.Value}' not found.");

        public static Error AlreadyExists(AccountId id) =>
            Error.Conflict($"Account with specified id '{id.Value}' already exists.");

        public static Error AlreadyExists(AccountName name) =>
            Error.Conflict($"Account with specified name '{name.Value}' already exists.");
    }

    public static class Orders
    {
        public static Error NotFound(OrderId id) =>
            Error.NotFound($"Order with specified id '{id.Value}' not found.");

        public static Error InvalidTimeInForce(OrderType type, TimeInForce timeInForce) =>
            Error.Invalid($"Invalid time in force for order type '{type}' and time in force '{timeInForce}'.");
    }
}