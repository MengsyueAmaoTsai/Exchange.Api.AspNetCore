namespace RichillCapital.SharedKernel.Monads;

public static partial class ErrorOrExtensions
{
    public static async Task<ErrorOr<TValue>> Then<TValue, TResult>(
        this ErrorOr<TValue> errorOr,
        Func<Task<TResult>> factoryTask)
    {
        if (errorOr.HasError)
        {
            return errorOr.Errors.ToErrorOr<TValue>();
        }

        _ = await factoryTask();

        return errorOr.Value.ToErrorOr();
    }
}