using Microsoft.Extensions.DependencyInjection;

namespace RichillCapital.DataFeeds;

public static class DependencyInjection
{
    public static IServiceCollection AddDataFeeds(this IServiceCollection services)
    {
        services.AddDataFeedOptions();

        services.AddSingleton<DataFeedProvider>();

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
}
