using Motorent.Application.Users.Queries.Get;
using Motorent.Presentation.Common.Endpoints;

namespace Motorent.Presentation.Users;

public sealed class Get : IEndpoint
{
    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        app.MapGet("users", (
                ISender sender,
                CancellationToken cancellationToken) => sender.Send(
                    new GetUserQuery(),
                    cancellationToken)
                .ToResponseAsync(Results.Ok))
            .RequireAuthorization();
    }
}