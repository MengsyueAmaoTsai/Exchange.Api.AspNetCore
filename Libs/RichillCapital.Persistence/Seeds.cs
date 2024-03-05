using System.Collections.ObjectModel;

using Microsoft.Extensions.DependencyInjection;

using RichillCapital.Domain.Trading;

namespace RichillCapital.Persistence;

public static class Seeds
{
    private static readonly Collection<Account> Accounts = [];
    private static readonly Collection<Order> Orders = [];
    private static readonly Collection<Execution> Executions = [];
    private static readonly Collection<Trade> Trades = [];

    public static void Initialize(IServiceProvider serviceProvider)
    {
        using var context = serviceProvider.GetRequiredService<PostgreSqlDbContext>();

        context.SaveChanges();
    }
}