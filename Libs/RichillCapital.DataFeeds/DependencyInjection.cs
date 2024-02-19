using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

using RichillCapital.DataFeeds.Exceptions;
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

    private static IServiceCollection EnsureNotDuplicateConnectionName(
        this IServiceCollection services,
        string providerName,
        string connectionName) =>
        services.Any(
            service => service.ServiceType == typeof(IDataFeed) &&
            service.ServiceKey?.ToString() == connectionName &&
            service.IsKeyedService) ?
            throw new DuplicateConnectionNameException(providerName, connectionName) :
            services;

    private static DataFeedOptions GetDataFeedOptions(this IServiceProvider serviceProvider) =>
        serviceProvider.GetRequiredService<IOptions<DataFeedOptions>>().Value;

    private static IServiceCollection AddDataFeedsFromConfiguration(this IServiceCollection services)
    {
        var options = services
            .BuildServiceProvider()
            .GetDataFeedOptions();

        var providerNames = options.Configurations
            .Select(configuration => configuration.ProviderName)
            .Distinct();

        foreach (var providerName in providerNames)
        {
            var connectionNames = options.Configurations
                .Where(configuration => configuration.ProviderName == providerName)
                .Select(configuration => configuration.ConnectionName);

            foreach (var connectionName in connectionNames)
            {
                services.EnsureNotDuplicateConnectionName(providerName, connectionName);

                switch (providerName)
                {
                    case "Max":
                        services.AddKeyedSingleton<IDataFeed, MaxDataFeed>(connectionName);
                        break;

                    case "Binance":
                        services.AddKeyedSingleton<IDataFeed, BinanceDataFeed>(connectionName);
                        break;

                    default:
                        throw new DataFeedNotSupportedException(connectionName, providerName);
                }
            }
        }

        return services;
    }
}
