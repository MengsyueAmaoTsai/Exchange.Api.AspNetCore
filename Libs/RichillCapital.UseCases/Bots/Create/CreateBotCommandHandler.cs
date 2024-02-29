using RichillCapital.Domain.Bots;
using RichillCapital.Domain.Common;
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
        CancellationToken cancellationToken)
    {
        var botIdResult = await BotId
            .From(command.Id)
            .Ensure(NotDuplicateAsync, BotErrors.Duplicate);

        if (botIdResult.IsFailure)
        {
            return botIdResult.Error
                .ToErrorOr<BotId>();
        }

        var botNameResult = await BotName
            .From(command.Name)
            .Ensure(NotDuplicateAsync, BotErrors.Duplicate);

        if (botNameResult.IsFailure)
        {
            return botNameResult.Error
                .ToErrorOr<BotId>();
        }

        var descriptionResult = BotDescription.From(command.Description);

        if (descriptionResult.IsFailure)
        {
            return descriptionResult.Error
                .ToErrorOr<BotId>();
        }

        var platformResult = TradingPlatform
            .FromName(command.Platform)
            .ToResult(BotErrors.TradingPlatformNotSupported(command.Platform));

        if (platformResult.IsFailure)
        {
            return platformResult.Error
                .ToErrorOr<BotId>();
        }

        // Create bot
        var errorOrBot = Bot
            .Create(
                botIdResult.Value,
                botNameResult.Value,
                descriptionResult.Value,
                platformResult.Value)
            .Then(_botRepository.Add);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return errorOrBot
            .Then(bot => bot.Id);
    }

    private async Task<bool> NotDuplicateAsync(BotId id) =>
        !await _botRepository.AnyAsync(bot => bot.Id == id);

    private async Task<bool> NotDuplicateAsync(BotName name) =>
        !await _botRepository.AnyAsync(bot => bot.Name == name);
}