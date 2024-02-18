namespace RichillCapital.ExchangeApi.Endpoints.Accounts;

public sealed record AccountPerformanceResponse(
    AccountPerformanceMetricsResponse Metrics);

public sealed record AccountPerformanceMetricsResponse(
    decimal SharpeRatio,
    decimal WinRate,
    decimal ProfitFactor);