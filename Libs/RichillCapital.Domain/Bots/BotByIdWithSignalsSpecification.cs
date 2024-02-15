using RichillCapital.SharedKernel.Specifications;

namespace RichillCapital.Domain.Bots;

public sealed class BotByIdWithSignalsSpecification : Specification<Bot>
{
    public BotByIdWithSignalsSpecification(BotId botId) =>
        Query
            .Where(bot => bot.Id == botId)
            .Include(bot => bot.Signals);
}