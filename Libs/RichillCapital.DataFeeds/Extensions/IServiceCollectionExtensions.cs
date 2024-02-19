using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

using RichillCapital.DataFeeds.Exceptions;

namespace RichillCapital.DataFeeds.Extensions;

internal static class IServiceCollectionExtensions
{
    public static IServiceCollection EnsureIsConnectionNameUnique(
        this IServiceCollection services,
        string connectionName) =>
        services.Any(
            service => service.ServiceType == typeof(IDataFeed) &&
            service.ServiceKey?.ToString() == connectionName &&
            service.IsKeyedService) ?
            throw new DuplicateConnectionNameException(connectionName) :
            services;

    public static IEnumerable<DataFeedConfiguration> GetConfigurations(this IServiceCollection services) =>
        services
            .BuildServiceProvider()
            .GetRequiredService<IOptions<DataFeedOptions>>().Value
            .Configurations;
}