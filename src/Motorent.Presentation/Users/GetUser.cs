using Motorent.Application.Users.Queries.GetUser;
using Motorent.Presentation.Common.Endpoints;

namespace Motorent.Presentation.Users;

public sealed class GetUser : IEndpoint
{
    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        app.MapGet("users", (
                ISender sender,
                CancellationToken cancellationToken) => sender.Send(
                    new GetUserQuery(),
                    cancellationToken)
                .ToResponseAsync(Results.Ok))
            .RequireAuthorization()
            .WithName("get-user");
    }
}