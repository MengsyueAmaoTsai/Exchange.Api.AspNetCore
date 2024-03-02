using RichillCapital.SharedKernel.Specifications;
using RichillCapital.SharedKernel.Specifications.Builders;

namespace RichillCapital.Domain.Bots;

public sealed class BotByIdWithSignalsSpecification : Specification<Bot>
{
    public BotByIdWithSignalsSpecification(BotId botId)
    {
        BotId = botId;

        Query
            .Where(bot => bot.Id == botId)
            .Include(bot => bot.Signals);
    }

    public BotId BotId { get; private init; }
}