using Microsoft.Extensions.DependencyInjection;

namespace RichillCapital.Messaging;

public static class DependencyInjection
{
    public static IServiceCollection AddMessaging(this IServiceCollection services)
    {
        services.AddPersistenceOptions();

        return services;
    }


    private static IServiceCollection AddPersistenceOptions(this IServiceCollection services)
    {
        services
            .AddOptions<MessagingOptions>()
            .BindConfiguration(nameof(MessagingOptions))
            .ValidateDataAnnotations()
            .ValidateOnStart();

        return services;
    }
}
