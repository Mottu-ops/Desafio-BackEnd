using Motorent.Domain.Common.ValueObjects;

namespace Motorent.Application.Common.Mappings;

internal sealed class CommonMappings : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Name, string>()
            .ConstructUsing(src => src.Value);

        config.NewConfig<Birthdate, DateOnly>()
            .ConstructUsing(src => src.Value);
    }
}