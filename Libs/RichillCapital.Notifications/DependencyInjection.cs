using Microsoft.Extensions.DependencyInjection;

using RichillCapital.UseCases.Common;

namespace RichillCapital.Notifications;

public static class DependencyInjection
{
    public static IServiceCollection AddNotifications(this IServiceCollection services)
    {
        services.AddTransient<INotificationService, NotificationService>();

        return services;
    }
}
