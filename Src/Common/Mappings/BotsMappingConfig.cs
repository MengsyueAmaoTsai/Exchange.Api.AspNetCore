using Mapster;

using RichillCapital.Exchange.Api.Endpoints.Bots;
using RichillCapital.UseCases.Bots.Create;

namespace RichillCapital.Common.Mappings;

public sealed class BotsMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config
            .NewConfig<CreateBotRequest, CreateBotCommand>()
                .Map(command => command.Id, request => request.Id)
                .Map(command => command.Name, request => request.Name)
                .Map(command => command.Description, request => request.Description)
                .Map(command => command.Platform, request => request.Platform);
    }
}