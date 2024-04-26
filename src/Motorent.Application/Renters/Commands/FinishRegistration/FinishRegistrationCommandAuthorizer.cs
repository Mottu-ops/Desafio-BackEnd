using Motorent.Application.Common.Security;
using Motorent.Application.Renters.Common.Security;
using Motorent.Domain.Users.Enums;

namespace Motorent.Application.Renters.Commands.FinishRegistration;

internal sealed class FinishRegistrationCommandAuthorizer : IAuthorizer<FinishRegistrationCommand>
{
    public IEnumerable<IAuthorizationRequirement> GetRequirements(FinishRegistrationCommand _)
    {
        yield return new RoleRequirement(Role.Renter);
        yield return new HasPendingRegistrationRequirement();
    }
}