using RichillCapital.SharedKernel.Monad;
using RichillCapital.UseCases.Common;

namespace RichillCapital.UseCases.Trading.ListAccounts;

public sealed record ListAccountsQuery() :
    IQuery<Result<IEnumerable<AccountDto>>>;