﻿using Microsoft.Extensions.DependencyInjection;

namespace RichillCapital.DataFeeds;

public static class DependencyInjection
{
    public static IServiceCollection AddDataFeeds(this IServiceCollection services)
    {
        return services;
    }
}
