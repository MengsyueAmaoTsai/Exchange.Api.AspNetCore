using System.Formats.Asn1;

namespace RichillCapital.SharedKernel.Monads;

public static partial class ErrorOrExtensions
{
    public static async Task<ErrorOr<TResult>> Then<TValue, TResult>(
        this Task<ErrorOr<TValue>> errorOrTask,
        Func<TValue, Task<ErrorOr<TResult>>> factory)
    {
        var errorOr = await errorOrTask;

        var errorOrResult = await factory(errorOr.Value);

        return errorOrResult;
    }

    public static async Task<ErrorOr<TResult>> Then<TValue, TResult>(
        this Task<ErrorOr<TValue>> errorOrTask,
        Func<TValue, TResult> factory)
    {
        var errorOr = await errorOrTask;

        var result = factory(errorOr.Value);

        return result.ToErrorOr();
    }
}