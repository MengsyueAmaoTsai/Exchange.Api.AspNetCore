using Microsoft.Extensions.DependencyInjection;

using RichillCapital.UseCases.Common;

namespace RichillCapital.Caching;

public static class DependencyInjection
{
    public static IServiceCollection AddCaching(this IServiceCollection services)
    {
        services
            .AddOptions<CachingOptions>()
            .BindConfiguration(nameof(CachingOptions))
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services.AddInMemoryCache();
        services.AddDistributedCache();

        return services;
    }

    private static IServiceCollection AddInMemoryCache(this IServiceCollection services)
    {
        services.AddMemoryCache();
        services.AddSingleton<IMemoryCacheService, MemoryCacheService>();

        return services;
    }

    private static IServiceCollection AddDistributedCache(this IServiceCollection services)
    {
        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = "127.0.0.1:6379";
            options.InstanceName = "richillcapital";
        });

        services.AddSingleton<IDistributedCacheService, DistributedCacheService>();

        return services;
    }
}
