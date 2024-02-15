using RichillCapital.Domain.Bots;
using RichillCapital.Domain.Common;
using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monad;
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
        var id = BotId.From(command.Id);

        if (id.IsError)
        {
            return id.Error;
        }

        var name = BotName.From(command.Name);

        if (name.IsError)
        {
            return name.Error;
        }

        var description = BotDescription.From(command.Description);

        if (description.IsError)
        {
            return description.Error;
        }

        var platform = TradingPlatform.FromName(command.Platform);

        if (platform.HasNoValue)
        {
            return Error.Invalid("Invalid platform.");
        }

        var bot = Bot.Create(
            id.Value,
            name.Value,
            description.Value,
            platform.Value);

        _botRepository.Add(bot);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return bot.Id;
    }
}