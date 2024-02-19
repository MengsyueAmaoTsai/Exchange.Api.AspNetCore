using System.Reflection;

using Microsoft.Extensions.Logging;

using RichillCapital.DataFeeds.Extensions;

namespace RichillCapital.DataFeeds;

public sealed class DataFeedProvider(
    ILogger<DataFeedProvider> _logger,
    IServiceProvider _serviceProvider)
{
    private readonly Dictionary<string, IDataFeed> _dataFeeds = [];

    public async Task InitializeAsync()
    {
        var options = _serviceProvider.GetDataFeedOptions();

        foreach (var configuration in options.Configurations)
        {
            var dataFeed = _serviceProvider
                .GetDataFeed(configuration.ConnectionName)
                .ApplyConfiguration(configuration);

            if (options.ConnectOnAppStart)
            {
                await dataFeed.ConnectAsync();
            }

            _dataFeeds.Add(dataFeed.ConnectionName, dataFeed);

            _logger.LogInformation(
                "Connection with name '{ConnectionName}' initialized. ProviderName: '{ProviderName}'.",
                dataFeed.ConnectionName,
                dataFeed.ProviderName);
        }
    }

    public static IEnumerable<Type> GetDataFeedTypes() =>
        AppDomain.CurrentDomain
            .GetAssemblies()
            .SelectMany(GetDataFeedTypes)
            .Where(type => type.IsDataFeedImplementation());

    public static IEnumerable<Type> GetDataFeedTypes(Assembly assembly) =>
        assembly.GetTypes()
            .Where(type => type.IsDataFeedImplementation());
}