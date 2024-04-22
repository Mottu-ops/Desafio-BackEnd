using Motorent.Application.Renters.Commands.FinishRegistration;
using Motorent.Contracts.Renters.Requests;
using Motorent.Presentation.Common.Endpoints;

namespace Motorent.Presentation.Renters;

public sealed class FinishRegistration : IEndpoint
{
    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        app.MapPost("renters/finish-registration", (
                FinishRegistrationRequest request,
                ISender sender,
                CancellationToken cancellationToken) => sender.Send(
                    request.Adapt<FinishRegistrationCommand>(),
                    cancellationToken)
                .ToResponseAsync(Results.Ok))
            .RequireAuthorization();
    }
}