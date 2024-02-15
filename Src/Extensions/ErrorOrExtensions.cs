using Microsoft.AspNetCore.Mvc;

using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monad;

namespace RichillCapital.Exchange.Api.Extensions;

public static class ErrorOrExtensions
{
    public static ActionResult Match<TValue>(
        this ErrorOr<TValue> errorOr,
        Func<Error, ActionResult> onIsError,
        Func<TValue, ActionResult> onNoError) =>
        errorOr.IsError ? onIsError(errorOr.Error) : onNoError(errorOr.Value);
}