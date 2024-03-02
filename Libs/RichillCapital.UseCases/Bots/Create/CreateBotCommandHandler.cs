using RichillCapital.Domain.Bots;
using RichillCapital.Domain.Common;
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
        CancellationToken cancellationToken)
    {
        var idResult = BotId.From(command.Id);

        if (idResult.IsFailure)
        {
            return idResult.Error
                .ToErrorOr<BotId>();
        }

        if (await _botRepository.AnyAsync(bot => bot.Id == idResult.Value))
        {
            return BotErrors.Duplicate(idResult.Value)
                .ToErrorOr<BotId>();
        }

        var nameResult = BotName.From(command.Name);

        if (nameResult.IsFailure)
        {
            return nameResult.Error
                .ToErrorOr<BotId>();
        }

        if (await _botRepository.AnyAsync(bot => bot.Name == nameResult.Value))
        {
            return BotErrors.Duplicate(nameResult.Value)
                .ToErrorOr<BotId>();
        }

        var descriptionResult = BotDescription.From(command.Description);

        if (descriptionResult.IsFailure)
        {
            return descriptionResult.Error
                .ToErrorOr<BotId>();
        }

        var maybeSide = Side.FromName(command.Side);

        if (maybeSide.IsNull)
        {
            return Error.Invalid("Side not supported.")
                .ToErrorOr<BotId>();
        }

        var maybePlatform = TradingPlatform.FromName(command.Platform);

        if (maybePlatform.IsNull)
        {
            return Error.Invalid("Trading platform not supported.")
                .ToErrorOr<BotId>();
        }

        throw new NotImplementedException();
        // return await Bot
        //     .Create(
        //         idResult.Value,
        //         nameResult.Value,
        //         descriptionResult.Value,
        //         maybeSide.Value,
        //         maybePlatform.Value)
        //     .Then(_botRepository.Add)
        //     .Then(() => _unitOfWork.SaveChangesAsync(cancellationToken))
        //     .Then(bot => bot.Id);
    }
}