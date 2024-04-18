using Motorent.Application.Common.Abstractions.Identity;
using Motorent.Application.Common.Abstractions.Requests;
using Motorent.Contracts.Users.Responses;
using Motorent.Domain.Users.Repository;

namespace Motorent.Application.Users.Queries.Get;

internal sealed class GetUserQueryHandler(IUserContext userContext, IUserRepository userRepository) 
    : IQueryHandler<GetUserQuery, UserResponse>
{
    public async Task<Result<UserResponse>> Handle(GetUserQuery _, CancellationToken cancellationToken)
    {
        var user = await userRepository.FindAsync(userContext.UserId, cancellationToken);
        return user is null
            ? throw new ApplicationException($"User '{userContext.UserId}' not found")
            : user.Adapt<UserResponse>();
    }
}