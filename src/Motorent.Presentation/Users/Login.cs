using Motorent.Application.Users.Commands.Login;
using Motorent.Contracts.Users.Requests;
using Motorent.Presentation.Common.Endpoints;

namespace Motorent.Presentation.Users;

public sealed class Login : IEndpoint
{
    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        app.MapPost("users/login", (
                LoginRequest request,
                ISender sender,
                CancellationToken cancellationToken) => sender.Send(
                    request.Adapt<LoginCommand>(),
                    cancellationToken)
                .ToResponseAsync(Results.Ok))
            .AllowAnonymous();
    }
}