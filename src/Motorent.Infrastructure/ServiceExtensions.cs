using Microsoft.Extensions.DependencyInjection;

namespace Motorent.Infrastructure;

public static class ServiceExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddAuthentication();
        services.AddAuthorization();
        
        return services;
    }
}