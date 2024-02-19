using RichillCapital.DataFeeds.Abstractions;

namespace RichillCapital.DataFeeds.Extensions;

public static class TypeExtensions
{
    public static bool IsDataFeedImplementation(this Type type) =>
        typeof(IDataFeed).IsAssignableFrom(type) &&
        (type.IsNotInterface() || type.IsNotAbstraction());

    public static bool IsNotInterface(this Type type) =>
        !type.IsInterface;

    public static bool IsNotAbstraction(this Type type) =>
        !type.IsAbstract;
}