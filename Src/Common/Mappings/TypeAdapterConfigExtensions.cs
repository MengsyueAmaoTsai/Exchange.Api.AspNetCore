using Mapster;

namespace RichillCapital.Common.Mappings;

public static class TypeAdapterConfigExtensions
{
    public static EndpointContractMapper MapEndpointContracts(this TypeAdapterConfig config) => new(config);
}