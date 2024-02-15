namespace RichillCapital.SharedKernel;

public enum ErrorType
{
    Default = 0,
    Failure = 1,
    Validation = 400,
    Unauthorized = 401,
    Forbidden = 403,
    NotFound = 404,
    Conflict = 409,
}