using Motorent.Application.Users.Commands.Refresh;
using Motorent.Contracts.Users.Requests;
using Motorent.Presentation.Common.Endpoints;

namespace Motorent.Presentation.Users;

public sealed class RefreshToken : IEndpoint
{
    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        app.MapPost("users/refresh-token", (
                RefreshTokenRequest request,
                ISender sender,
                CancellationToken cancellationToken) => sender.Send(
                    request.Adapt<RefreshTokenCommand>(),
                    cancellationToken)
                .ToResponseAsync(Results.Ok))
            .AllowAnonymous();
    }
}