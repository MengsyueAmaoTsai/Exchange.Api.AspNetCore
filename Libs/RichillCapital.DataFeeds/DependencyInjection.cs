using Microsoft.Extensions.DependencyInjection;

using RichillCapital.DataFeeds.Exceptions;
using RichillCapital.DataFeeds.Extensions;
using RichillCapital.DataFeeds.Max;

namespace RichillCapital.DataFeeds;

public static class DependencyInjection
{
    public static IServiceCollection AddDataFeeds(this IServiceCollection services)
    {
        services.AddDataFeedOptions();

        services.AddSingleton<DataFeedProvider>();

        services.AddDataFeedsFromConfiguration();

        return services;
    }

    private static IServiceCollection AddDataFeedOptions(this IServiceCollection services)
    {
        services
            .AddOptions<DataFeedOptions>()
        .BindConfiguration(nameof(DataFeedOptions))
        .ValidateDataAnnotations()
        .ValidateOnStart();

        return services;
    }

    private static IServiceCollection AddDataFeedsFromConfiguration(this IServiceCollection services)
    {
        var configurations = services.GetConfigurations();

        var providerNames = configurations
            .Select(configuration => configuration.ProviderName)
            .Distinct();

        foreach (var providerName in providerNames)
        {
            var connectionNames = configurations
                .Where(configuration => configuration.ProviderName == providerName)
                .Select(configuration => configuration.ConnectionName);

            foreach (var connectionName in connectionNames)
            {
                services.AddConnection(providerName, connectionName);
            }
        }

        return services;
    }

    private static IServiceCollection AddConnection(
        this IServiceCollection services,
        string providerName,
        string connectionName)
    {
        services.EnsureIsConnectionNameUnique(connectionName);

        switch (providerName)
        {
            case DataFeedProviders.Max:
                services.AddKeyedSingleton<IDataFeed, MaxDataFeed>(connectionName);
                break;

            case DataFeedProviders.Binance:
                services.AddKeyedSingleton<IDataFeed, BinanceDataFeed>(connectionName);
                break;

            default:
                throw new DataFeedNotSupportedException(connectionName, providerName);
        }

        return services;
    }
}
