using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

using RichillCapital.SharedKernel;

namespace RichillCapital.Exchange.Api.Common;

[ApiController]
public abstract class Endpoint : ControllerBase
{
    protected virtual ActionResult HandleError(IEnumerable<Error> errors) =>
       !errors.Any() ?
           HandleError(Error.Null) :
           errors.All(IsValidationError) ?
               HandleValidationErrors(errors) :
               HandleError(errors.First());

    protected virtual ActionResult HandleError(Error error)
    {
        return Problem(
            statusCode: StatusCodeOf(error.Type),
            title: TitleOf(error.Type),
            type: TypeOf(error.Type),
            detail: error.Message);

        static int StatusCodeOf(ErrorType errorType) =>
            errorType switch
            {
                ErrorType.Validation => StatusCodes.Status400BadRequest,
                ErrorType.Unauthorized => StatusCodes.Status401Unauthorized,
                ErrorType.Forbidden => StatusCodes.Status403Forbidden,
                ErrorType.NotFound => StatusCodes.Status404NotFound,
                ErrorType.Conflict => StatusCodes.Status409Conflict,
                _ => StatusCodes.Status500InternalServerError,
            };

        static string TitleOf(ErrorType errorType) =>
            errorType switch
            {
                ErrorType.Validation => "Bad Request",
                ErrorType.Unauthorized => "Unauthorized",
                ErrorType.Forbidden => "Forbidden",
                ErrorType.NotFound => "Not Found",
                ErrorType.Conflict => "Conflict",
                _ => "Server Failure",
            };

        static string TypeOf(ErrorType errorType) =>
            errorType switch
            {
                ErrorType.Validation => "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1",
                ErrorType.Unauthorized => "https://datatracker.ietf.org/doc/html/rfc7235#section-3.1",
                ErrorType.Forbidden => "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.3",
                ErrorType.NotFound => "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.4",
                ErrorType.Conflict => "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.8",
                _ => "https://datatracker.ietf.org/doc/html/rfc7231#section-6.6.1",
            };
    }

    private ActionResult HandleValidationErrors(IEnumerable<Error> errors)
    {
        var modelStateDictionary = new ModelStateDictionary();

        foreach (var error in errors)
        {
            modelStateDictionary.AddModelError(error.Message, error.Message);
        }

        return ValidationProblem(modelStateDictionary);
    }

    private bool IsValidationError(Error error) =>
        error.Type == ErrorType.Validation;
}