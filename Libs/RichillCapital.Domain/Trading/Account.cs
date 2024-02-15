using RichillCapital.SharedKernel;

namespace RichillCapital.Domain.Trading;

public sealed class Account : Entity<AccountId>
{
    private readonly List<AccountBalance> _balances = [];
    private readonly List<Order> _orders = [];
    private readonly List<Execution> _executions = [];
    private readonly List<Position> _positions = [];
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

    public IReadOnlyList<AccountBalance> Balances => _balances.AsReadOnly();

    public IReadOnlyList<Order> Orders => _orders.AsReadOnly();

    public IReadOnlyList<Execution> Executions => _executions.AsReadOnly();

    public IReadOnlyList<Position> Positions => _positions.AsReadOnly();

    public IReadOnlyList<Trade> Trades => _trades.AsReadOnly();

    public static Account Create(
        AccountName name,
        Currency currency)
    {
        var account = new Account(
            AccountId.NewAccountId(),
            name,
            currency);

        account.RegisterDomainEvent(new AccountCreatedDomainEvent(account.Id));

        return account;
    }
}