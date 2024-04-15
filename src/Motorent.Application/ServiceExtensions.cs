using Microsoft.Extensions.DependencyInjection;

namespace Motorent.Application;

public static class ServiceExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        return services;
    }
}