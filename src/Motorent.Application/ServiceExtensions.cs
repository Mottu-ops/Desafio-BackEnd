using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Motorent.Application;

public static class ServiceExtensions
{
    private static readonly Assembly ApplicationAssembly = typeof(ServiceCollectionExtensions).Assembly;
    
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediator();
        
        return services;
    }
    
    private static void AddMediator(this IServiceCollection services) => services.AddMediatR(config =>
    {
        config.RegisterServicesFromAssembly(ApplicationAssembly);
    });
}