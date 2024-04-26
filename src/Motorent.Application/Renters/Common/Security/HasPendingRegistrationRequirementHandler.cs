using Motorent.Application.Common.Abstractions.Identity;
using Motorent.Domain.Renters.Repository;

namespace Motorent.Application.Renters.Common.Security;

internal sealed class HasPendingRegistrationRequirementHandler(
    IUserContext userContext,
    IRenterRepository renterRepository) 
    : IAuthorizationRequirementHandler<HasPendingRegistrationRequirement>
{
    public async Task<AuthorizationResult> HandleAsync(HasPendingRegistrationRequirement _,
        CancellationToken cancellationToken = default)
    {
        return await renterRepository.ExistsByUserAsync(userContext.UserId, cancellationToken)
            ? AuthorizationResult.Failure("Renter has no pending registration")
            : AuthorizationResult.Success();
    }
}