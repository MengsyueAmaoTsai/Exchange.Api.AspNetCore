namespace RichillCapital.UseCases.Trading;

public sealed record AccountBalanceDto(
    string Currency,
    decimal Amount,
    string AccountId);