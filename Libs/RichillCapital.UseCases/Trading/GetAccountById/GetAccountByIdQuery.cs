using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Common;

namespace RichillCapital.UseCases.Trading.GetAccountById;

public sealed record GetAccountByIdQuery(
    string AccountId) :
    IQuery<ErrorOr<AccountDto>>;