using Microsoft.AspNetCore.Routing;

namespace Motorent.Presentation.Common.Endpoints;

public interface IEndpoint
{
    void MapEndpoints(IEndpointRouteBuilder app);
}