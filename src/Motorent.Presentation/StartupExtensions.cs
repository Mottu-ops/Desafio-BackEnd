using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Motorent.Presentation.Common.Endpoints;

namespace Motorent.Presentation;

public static class StartupExtensions
{
    public static void UsePresentation(this WebApplication app)
    {
        app.UseHttpsRedirection();
        
        app.MapEndpoints();
    }

    private static void MapEndpoints(this WebApplication app)
    {
        var routeGroup = app.MapGroup("api");
        
        foreach (var endpoint in app.Services.GetRequiredService<IEnumerable<IEndpoint>>())
        {
            endpoint.MapEndpoints(routeGroup);
        }
    }
}