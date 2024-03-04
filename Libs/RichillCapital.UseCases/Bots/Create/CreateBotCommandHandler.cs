using RichillCapital.Domain.Bots;
using RichillCapital.Domain.Common;
using RichillCapital.Domain.Shared;
using RichillCapital.Domain.Trading;
using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Common;

namespace RichillCapital.UseCases.Bots.Create;

internal sealed class CreateBotCommandHandler(
    IRepository<Bot> _botRepository,
    IUnitOfWork _unitOfWork)
    : ICommandHandler<CreateBotCommand, ErrorOr<BotId>>
{
    public async Task<ErrorOr<BotId>> Handle(
        CreateBotCommand command,
        CancellationToken cancellationToken) =>
        await Result<(BotId, BotName, NonEmptyDescription, Side, TradingPlatform)>
            .Combine(
                await BotId.From(command.Id).Ensure(NotDuplicate, BotErrors.Duplicate),
                await BotName.From(command.Name).Ensure(NotDuplicate, BotErrors.Duplicate),
                NonEmptyDescription.From(command.Description),
                Side.FromName(command.Side).ToResult(Error.Invalid("Invalid side.")),
                TradingPlatform.FromName(command.Platform).ToResult(Error.Invalid("Invalid platform.")))
            .ToErrorOr()
            .Then(CreateBot)
            .Then(_botRepository.Add)
            .Then(() => _unitOfWork.SaveChangesAsync(cancellationToken))
            .Then(bot => bot.Id);

    private async Task<bool> NotDuplicate(BotId id) => !await _botRepository.AnyAsync(bot => bot.Id == id);

    private async Task<bool> NotDuplicate(BotName name) => !await _botRepository.AnyAsync(bot => bot.Name == name);

    private static ErrorOr<Bot> CreateBot((
        BotId id,
        BotName name,
        NonEmptyDescription description,
        Side side,
        TradingPlatform platform) parameters) =>
        Bot.Create(
            parameters.id,
            parameters.name,
            parameters.description,
            parameters.side,
            parameters.platform);
}