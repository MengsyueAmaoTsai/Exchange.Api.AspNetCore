using Microsoft.Extensions.DependencyInjection;

using RichillCapital.Domain.Bots;

namespace RichillCapital.Domain;

public static class DependencyInjection
{
    public static IServiceCollection AddDomainServices(this IServiceCollection services)
    {
        services.AddScoped<BotAccountsService>();

        return services;
    }
}
