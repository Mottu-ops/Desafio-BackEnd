using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Motorent.Application.Users.Commands.Register;
using Motorent.Contracts.Users.Requests;
using Motorent.Presentation.Common.Endpoints;

namespace Motorent.Presentation.Users;

public sealed class Register : IEndpoint
{
    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        app.MapPost("users/register", (
                RegisterRequest request,
                ISender sender,
                CancellationToken cancellationToken) => sender.Send(
                    request.Adapt<RegisterCommand>(),
                    cancellationToken)
                .ToResponseAsync(Results.Ok))
            .AllowAnonymous();
    }
}