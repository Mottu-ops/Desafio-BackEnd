using Motorent.Domain.Users.Enums;

namespace Motorent.Application.Users.Common.Mappings;

internal sealed class UserMappings : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Role, string>()
            .MapWith(x => x.Name);
    }
}