using Asp.Versioning;
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
        var versionSet = app.NewApiVersionSet()
            .HasApiVersion(new ApiVersion(1))
            .ReportApiVersions()
            .Build();
        
        var routeGroup = app.MapGroup("api/v{version:apiVersion}")
            .WithApiVersionSet(versionSet);
        
        foreach (var endpoint in app.Services.GetRequiredService<IEnumerable<IEndpoint>>())
        {
            endpoint.MapEndpoints(routeGroup);
        }
    }
}