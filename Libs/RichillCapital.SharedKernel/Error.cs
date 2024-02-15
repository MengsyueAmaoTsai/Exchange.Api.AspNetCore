namespace RichillCapital.SharedKernel;

public readonly record struct Error
{
    public static readonly Error Default = new(ErrorType.Default, string.Empty);
    public static readonly Error NullValue = new(ErrorType.Failure, "Null value was provided.");

    private Error(ErrorType type, string message)
    {
        Type = type;
        Message = message;
    }

    public ErrorType Type { get; private init; }

    public string Message { get; private init; }

    public static Error Invalid(string message) => new(ErrorType.Validation, message);
    public static Error Unauthorized(string message) => new(ErrorType.Unauthorized, message);
    public static Error Forbidden(string message) => new(ErrorType.Forbidden, message);
    public static Error NotFound(string message) => new(ErrorType.NotFound, message);
    public static Error Conflict(string message) => new(ErrorType.Conflict, message);
}
