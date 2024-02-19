namespace RichillCapital.DataFeeds.Extensions;

public static class TypeExtensions
{
    /// <summary>
    /// Check if a type is a data feed implementation or not
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public static bool IsDataFeedImplementation(this Type type) =>
        typeof(IDataFeed).IsAssignableFrom(type) &&
        (type.IsNotInterface() || type.IsNotAbstraction());

    public static bool IsNotInterface(this Type type) =>
        !type.IsInterface;

    public static bool IsNotAbstraction(this Type type) =>
        !type.IsAbstract;
}