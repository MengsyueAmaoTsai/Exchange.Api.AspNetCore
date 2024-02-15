namespace RichillCapital.SharedKernel;

public sealed record class ErrorType : Enumeration<ErrorType>
{
    public static readonly ErrorType Default = new(nameof(Default), 0);
    public static readonly ErrorType Validation = new(nameof(Validation), 400);
    public static readonly ErrorType Unauthorized = new(nameof(Unauthorized), 401);
    public static readonly ErrorType Forbidden = new(nameof(Forbidden), 403);
    public static readonly ErrorType NotFound = new(nameof(NotFound), 404);
    public static readonly ErrorType Conflict = new(nameof(Conflict), 409);
    public static readonly ErrorType Failure = new(nameof(Failure), 500);

    private ErrorType(string name, int value)
        : base(name, value)
    {
    }
}