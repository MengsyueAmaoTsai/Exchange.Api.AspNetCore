using System.Reflection;

using RichillCapital.SharedKernel.Monad;

namespace RichillCapital.SharedKernel;

public abstract record class Enumeration<TEnum> :
    Enumeration<TEnum, int>
    where TEnum : Enumeration<TEnum, int>
{
    protected Enumeration(string name, int value)
        : base(name, value)
    {
    }
}

public abstract record class Enumeration<TEnum, TValue>
    where TEnum : Enumeration<TEnum, TValue>
{
    public static IReadOnlyCollection<TEnum> Members =>
        _fromName.Value.Values.ToList().AsReadOnly();

    private static readonly Lazy<TEnum[]> _enumOptions =
        new(GetAllOptions, LazyThreadSafetyMode.ExecutionAndPublication);

    private static readonly Lazy<Dictionary<string, TEnum>> _fromName =
        new(() => _enumOptions.Value.ToDictionary(item => item.Name));

    private static readonly Lazy<Dictionary<string, TEnum>> _fromNameIgnoreCase =
        new(() => _enumOptions.Value.ToDictionary(
            item => item.Name,
            StringComparer.OrdinalIgnoreCase));

    private static readonly Lazy<Dictionary<TValue, TEnum>> _fromValue =
        new(() =>
        {
            var dictionary = new Dictionary<TValue, TEnum>(GetValueComparer());

            foreach (var item in _enumOptions.Value)
            {
                if (item.Value != null && !dictionary.ContainsKey(item.Value))
                {
                    dictionary.Add(item.Value, item);
                }
            }

            return dictionary;
        });

    protected Enumeration(string name, TValue value)
    {
        Name = name;
        Value = value;
    }

    public string Name { get; private init; }

    public TValue Value { get; private init; }

    public static Maybe<TEnum> FromName(string name, bool ignoreCase = false) =>
        string.IsNullOrWhiteSpace(name)
            ? Maybe<TEnum>.Null
            : (ignoreCase ?
                _fromNameIgnoreCase.Value :
                _fromName.Value).TryGetValue(name, out var enumeration) ?
            enumeration :
            Maybe<TEnum>.Null;

    public static Maybe<TEnum> FromValue(TValue value) =>
        value is not null && _fromValue.Value.TryGetValue(value, out var enumeration) ?
            enumeration :
            _enumOptions.Value.FirstOrDefault(x => x.Value is null) ??
            Maybe<TEnum>.Null;

    public override string ToString() => Name;

    public override int GetHashCode() => Value.GetHashCode();

    private static TEnum[] GetAllOptions()
    {
        var enumType = typeof(TEnum);
        var assembly = Assembly.GetAssembly(enumType) ?? throw new InvalidOperationException();

        return assembly.GetTypes()
            .Where(enumType.IsAssignableFrom)
            .SelectMany(type => type.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
                .Where(field => type.IsAssignableFrom(field.FieldType))
                .Select(field => (TEnum)field.GetValue(null)!)
                .ToList())
            .OrderBy(enumeration => enumeration.Name)
            .ToArray();
    }

    private static IEqualityComparer<TValue>? GetValueComparer() =>
        typeof(TEnum).GetCustomAttribute<EnumerationComparerAttribute<TValue>>()?.Comparer;
}

[AttributeUsage(AttributeTargets.Class)]
public class EnumerationComparerAttribute<T> : Attribute
{
    protected EnumerationComparerAttribute(IEqualityComparer<T> comparer)
        => Comparer = comparer;

    public IEqualityComparer<T> Comparer { get; private init; }
}

public sealed class EnumerationStringComparerAttribute : EnumerationComparerAttribute<string>
{
    public EnumerationStringComparerAttribute(StringComparison comparison)
        : base(GetComparer(comparison))
    {
    }

    public StringComparison Comparison { get; }

    private static StringComparer GetComparer(StringComparison comparison) =>
        comparison switch
        {
            StringComparison.Ordinal => StringComparer.Ordinal,
            StringComparison.OrdinalIgnoreCase => StringComparer.OrdinalIgnoreCase,
            StringComparison.CurrentCulture => StringComparer.CurrentCulture,
            StringComparison.CurrentCultureIgnoreCase => StringComparer.CurrentCultureIgnoreCase,
            StringComparison.InvariantCulture => StringComparer.InvariantCulture,
            StringComparison.InvariantCultureIgnoreCase => StringComparer.InvariantCultureIgnoreCase,
            _ => throw new ArgumentException($"StringComparison {comparison} is not supported", nameof(comparison)),
        };
}