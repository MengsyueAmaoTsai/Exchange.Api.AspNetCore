using Microsoft.Extensions.DependencyInjection;

namespace RichillCapital.Persistence;

public static class Seeds
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using var context = serviceProvider.GetRequiredService<PostgreSqlOptionsDbContext>();

        context.SaveChanges();
    }
}