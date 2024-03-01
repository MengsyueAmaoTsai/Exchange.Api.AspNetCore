using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;

namespace RichillCapital.SharedKernel.Monads;

public static partial class ResultExtensions
{
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Result<TValue> ToResult<TValue>(this Maybe<TValue> maybe, Func<TValue, Error> errorFactory) =>
        maybe.Match(Result<TValue>.Success, () => Result<TValue>.Failure(errorFactory(maybe.ValueOrDefault)));
}