using RichillCapital.Domain;
using RichillCapital.Domain.Bots;
using RichillCapital.Domain.Common;
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
        var id = BotId.From(command.Id);

        if (id.IsError)
        {
            return id.Errors.ToList();
        }

        if (await _botRepository.AnyAsync(bot => bot.Id == id.Value, cancellationToken))
        {
            return BotErrors.Duplicate(id.Value);
        }

        var name = BotName.From(command.Name);

        if (name.IsError)
        {
            return name.Errors.ToList();
        }

        if (await _botRepository.AnyAsync(
            bot => bot.Name == name.Value,
            cancellationToken))
        {
            return BotErrors.Duplicate(name.Value);
        }

        var description = BotDescription.From(command.Description);

        if (description.IsError)
        {
            return description.Errors.ToList();
        }

        var platform = TradingPlatform.FromName(command.Platform);

        if (platform.HasNoValue)
        {
            return BotErrors.TradingPlatformNotSupported(command.Platform);
        }

        var bot = Bot.Create(
            id.Value,
            name.Value,
            description.Value,
            platform.Value);

        _botRepository.Add(bot);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return ErrorOr<BotId>.
            Is(bot.Id);
    }
}