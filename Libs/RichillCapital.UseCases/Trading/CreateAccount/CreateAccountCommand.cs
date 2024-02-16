using RichillCapital.Domain.Trading;
using RichillCapital.SharedKernel.Monad;
using RichillCapital.UseCases.Common;

namespace RichillCapital.UseCases.Trading.CreateAccount;

public sealed record CreateAccountCommand(
    string Name,
    string PositionMode,
    string Currency,
    decimal InitialDeposit) :
    ICommand<ErrorOr<AccountId>>;