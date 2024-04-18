using System.Text.Json;
using Asp.Versioning;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Motorent.Presentation.Common.Endpoints;

namespace Motorent.Presentation;

public static class ServiceExtensions
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.Configure<JsonOptions>(options =>
        {
            options.SerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower;
        });

        services.AddApiVersioning(options =>
        {
            options.ReportApiVersions = true;
            options.DefaultApiVersion = new ApiVersion(1);
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.ApiVersionReader = new UrlSegmentApiVersionReader();
        });

        services.AddEndpoints();

        return services;
    }

    private static void AddEndpoints(this IServiceCollection services)
    {
        var assembly = typeof(ServiceExtensions).Assembly;
        var serviceDescriptors = assembly.DefinedTypes
            .Where(type => type is { IsAbstract: false, IsInterface: false }
                           && type.IsAssignableTo(typeof(IEndpoint)))
            .Select(type => ServiceDescriptor.Transient(typeof(IEndpoint), type))
            .ToArray();

        services.TryAddEnumerable(serviceDescriptors);
    }
}