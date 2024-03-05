using System.Collections.ObjectModel;

using Microsoft.Extensions.DependencyInjection;

using RichillCapital.Domain.Shared;
using RichillCapital.Domain.Trading;

namespace RichillCapital.Persistence;

public static class Seeds
{
    private static readonly Collection<Account> Accounts = [
        Account.Create(AccountId.From("1").Value, AccountName.From("test1").Value, PositionMode.Hedging, TradingEnvironment.Mock, Currency.TWD).Value,
        Account.Create(AccountId.From("1").Value, AccountName.From("test2").Value, PositionMode.Hedging, TradingEnvironment.Simulated, Currency.TWD).Value,
    ];

    private static readonly Collection<Order> Orders = [];
    private static readonly Collection<Execution> Executions = [];
    private static readonly Collection<Trade> Trades = [];

    public static void Initialize(IServiceProvider serviceProvider)
    {
        using var context = serviceProvider.GetRequiredService<PostgreSqlDbContext>();

        context.Set<Account>().AddRange(Accounts);

        context.SaveChanges();
    }
}