using Mapster;

using RichillCapital.Domain.Bots;
using RichillCapital.Exchange.Api.Endpoints.Bots;
using RichillCapital.UseCases.Bots.Create;
using RichillCapital.UseCases.Bots.EmitSignal;

namespace RichillCapital.Common.Mappings;

public sealed class BotsMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config) => config
        .MapEndpointContracts().CreateBot()
        .MapEndpointContracts().EmitBotSignal();
}

public sealed partial class EndpointContractMapper(TypeAdapterConfig _config)
{
    public TypeAdapterConfig CreateBot()
    {
        _config.NewConfig<CreateBotRequest, CreateBotCommand>()
            .Map(command => command.Id, request => request.Id)
            .Map(command => command.Name, request => request.Name)
            .Map(command => command.Description, request => request.Description)
            .Map(command => command.Side, request => request.Side)
            .Map(command => command.Platform, request => request.Platform);

        _config.NewConfig<BotId, CreateBotResponse>()
            .Map(response => response.BotId, id => id.Value);

        return _config;
    }

    public TypeAdapterConfig EmitBotSignal()
    {
        _config.NewConfig<EmitSignalRequest, EmitBotSignalCommand>()
            .Map(command => command.Time, request => request.Body.Time)
            .Map(command => command.TradeType, request => request.Body.TradeType)
            .Map(command => command.Symbol, request => request.Body.Symbol)
            .Map(command => command.Volume, request => request.Body.Volume)
            .Map(command => command.Price, request => request.Body.Price)
            .Map(command => command.BotId, request => request.BotId);

        _config.NewConfig<BotId, EmitSignalResponse>()
            .Map(response => response.BotId, id => id.Value);

        return _config;
    }
}