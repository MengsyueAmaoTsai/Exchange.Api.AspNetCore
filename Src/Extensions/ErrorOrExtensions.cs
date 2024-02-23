using Microsoft.AspNetCore.Mvc;

using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monad;

namespace RichillCapital.Exchange.Api.Extensions;

public static class ErrorOrExtensions
{
    public static ErrorOr<TDestination> Map<TSource, TDestination>(
        this ErrorOr<TSource> source,
        Func<TSource, TDestination> map) =>
        source.IsError ?
            ErrorOr<TDestination>.From(source.Errors) :
            ErrorOr<TDestination>.Is(map(source.Value));

    public static async Task<ErrorOr<TDestination>> Map<TSource, TDestination>(
        this Task<ErrorOr<TSource>> sourceTask,
        Func<TSource, TDestination> map)
    {
        var errorOr = await sourceTask;

        return errorOr.IsError ?
            ErrorOr<TDestination>.From(errorOr.Errors) :
            ErrorOr<TDestination>.Is(map(errorOr.Value));
    }

    public static async Task<ErrorOr<TResult>> Then<TValue, TResult>(
        this ErrorOr<TValue> errorOr,
        Func<TValue, Task<ErrorOr<TResult>>> errorOrTask) =>
        errorOr.IsError ?
            ErrorOr<TResult>.From(errorOr.Errors) :
            await errorOrTask(errorOr.Value);

    public static TResult Match<TValue, TResult>(
        this ErrorOr<TValue> errorOr,
        Func<IEnumerable<Error>, TResult> onIsError,
        Func<TValue, TResult> onIsValue) =>
        errorOr.IsError ?
            onIsError(errorOr.Errors) :
            onIsValue(errorOr.Value);

    public static async Task<TResult> Match<TValue, TResult>(
        this Task<ErrorOr<TValue>> errorOrTask,
        Func<IEnumerable<Error>, TResult> onIsError,
        Func<TValue, TResult> onIsValue)
    {
        var errorOr = await errorOrTask;

        return errorOr.IsError ?
            onIsError(errorOr.Errors) :
            onIsValue(errorOr.Value);
    }
}

public static class ResultExtensions
{
    public static Result<TDestination> Map<TSource, TDestination>(
        this Result<TSource> source,
        Func<TSource, TDestination> map) =>
        source.IsFailure ?
            Result<TDestination>.Failure(source.Error) :
            Result<TDestination>.Success(map(source.Value));

    public static async Task<Result<TDestination>> Map<TSource, TDestination>(
        this Task<Result<TSource>> sourceTask,
        Func<TSource, TDestination> map)
    {
        var result = await sourceTask;

        return result.IsFailure ?
            Result<TDestination>.Failure(result.Error) :
            Result<TDestination>.Success(map(result.Value));
    }

    public static async Task<Result<TResult>> Then<TValue, TResult>(
        this Result<TValue> result,
        Func<TValue, Task<Result<TResult>>> resultTask) =>
        result.IsFailure ?
            Result<TResult>.Failure(result.Error) :
            await resultTask(result.Value);

    public static TResult Match<TValue, TResult>(
        this Result<TValue> errorOr,
        Func<TValue, TResult> onSuccess,
        Func<Error, TResult> onFailure) =>
        errorOr.IsFailure ?
            onFailure(errorOr.Error) :
            onSuccess(errorOr.Value);

    public static async Task<TResult> Match<TValue, TResult>(
        this Task<Result<TValue>> resultTask,
        Func<TValue, TResult> onSuccess,
        Func<Error, TResult> onFailure)
    {
        var result = await resultTask;

        return result.IsFailure ?
            onFailure(result.Error) :
            onSuccess(result.Value);
    }
}