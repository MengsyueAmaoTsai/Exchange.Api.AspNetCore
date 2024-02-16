namespace RichillCapital.UseCases.Trading;

public sealed record AccountDto(
    string Id,
    string Name,
    string PositionMode,
    string Currency,
    IEnumerable<AccountBalanceDto> Balance);