using Microsoft.AspNetCore.Mvc;

using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monad;

namespace RichillCapital.Exchange.Api.Extensions;

public static class ResultExtensions
{
    public static ActionResult Match<TValue>(
        this Result<TValue> result,
        Func<TValue, ActionResult> onSuccess,
        Func<Error, ActionResult> onFailure) =>
        result.IsSuccess ? onSuccess(result.Value) : onFailure(result.Error);
}