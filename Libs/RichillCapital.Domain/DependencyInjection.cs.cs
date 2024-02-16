using Microsoft.Extensions.DependencyInjection;

using RichillCapital.Domain.Bots;
using RichillCapital.Domain.Trading;

namespace RichillCapital.Domain;

public static class DependencyInjection
{
    public static IServiceCollection AddDomainServices(this IServiceCollection services)
    {
        services.AddScoped<BotAccountsService>();

        services.AddScoped<OrderMatchingService>();

        return services;
    }
}
