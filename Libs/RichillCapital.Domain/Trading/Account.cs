using RichillCapital.SharedKernel;

namespace RichillCapital.Domain.Trading;

public sealed class Account : Entity<AccountId>
{
    private readonly List<Trade> _trades = [];

    private Account(
        AccountId id,
        AccountName name,
        Currency currency)
        : base(id)
    {
        Name = name;
        Currency = currency;
    }

    public AccountName Name { get; private set; }

    public Currency Currency { get; private set; }

    public IReadOnlyList<Trade> Trades => _trades.AsReadOnly();

    public static Account Create(
        AccountId id,
        AccountName name,
        Currency currency)
    {
        var account = new Account(id, name, currency);

        account.RegisterDomainEvent(new AccountCreatedDomainEvent(account.Id));

        return account;
    }
}