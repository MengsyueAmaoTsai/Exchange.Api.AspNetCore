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
        var idResult = BotId.From(command.Id);

        if (idResult.IsFailure)
        {
            return idResult.Error.ToErrorOr<BotId>();
        }

        if (await _botRepository.AnyAsync(bot => bot.Id == idResult.Value, cancellationToken))
        {
            return BotErrors
                .Duplicate(idResult.Value)
                .ToErrorOr<BotId>();
        }

        var nameResult = BotName.From(command.Name);

        if (nameResult.IsFailure)
        {
            return nameResult.Error
                .ToErrorOr<BotId>();
        }

        if (await _botRepository.AnyAsync(
            bot => bot.Name == nameResult.Value,
            cancellationToken))
        {
            return BotErrors
                .Duplicate(nameResult.Value)
                .ToErrorOr<BotId>();
        }

        var description = BotDescription.From(command.Description);

        if (description.IsFailure)
        {
            return description.Error
                .ToErrorOr<BotId>();
        }

        var platformMaybe = TradingPlatform.FromName(command.Platform);

        if (platformMaybe.HasNoValue)
        {
            return BotErrors
                .TradingPlatformNotSupported(command.Platform)
                .ToErrorOr<BotId>();
        }

        var bot = Bot.Create(
            idResult.Value,
            nameResult.Value,
            description.Value,
            platformMaybe.Value);

        _botRepository.Add(bot);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return ErrorOr<BotId>.
            Is(bot.Id);
    }
}