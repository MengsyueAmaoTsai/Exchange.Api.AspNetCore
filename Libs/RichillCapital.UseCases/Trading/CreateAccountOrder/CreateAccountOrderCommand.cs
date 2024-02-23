using RichillCapital.Domain.Trading;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Common;

namespace RichillCapital.UseCases.Trading.CreateAccountOrder;

public sealed record CreateAccountOrderCommand(
    string TradeType,
    decimal Quantity,
    string Symbol,
    string OrderType,
    string TimeInForce,
    string AccountId) :
    ICommand<ErrorOr<OrderId>>;