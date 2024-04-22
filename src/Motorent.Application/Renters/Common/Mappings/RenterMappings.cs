using Motorent.Contracts.Renters.Responses;
using Motorent.Domain.Renters;
using Motorent.Domain.Renters.Enums;
using Motorent.Domain.Renters.ValueObjects;

namespace Motorent.Application.Renters.Common.Mappings;

public sealed class RenterMappings : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Cnpj, string>()
            .ConstructUsing(src => src.Value);

        config.NewConfig<CnhNumber, string>()
            .ConstructUsing(src => src.Value);

        config.NewConfig<CnhCategory, string>()
            .ConstructUsing(src => src.Name);

        config.NewConfig<Renter, RenterResponse>()
            .Map(dest => dest.CnhNumber, src => src.Cnh.Number)
            .Map(dest => dest.CnhCategory, src => src.Cnh.Category)
            .Map(dest => dest.CnhExpirationDate, src => src.Cnh.ExpirationDate);
    }
}