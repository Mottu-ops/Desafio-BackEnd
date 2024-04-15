using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Motorent.Application.Common.Validations;

namespace Motorent.Application;

public static class ServiceExtensions
{
    private static readonly Assembly ApplicationAssembly = typeof(ServiceExtensions).Assembly;
    
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediator();

        services.AddValidator();
        
        return services;
    }
    
    private static void AddMediator(this IServiceCollection services) => services.AddMediatR(config =>
    {
        config.RegisterServicesFromAssembly(ApplicationAssembly);
    });
    
    private static void AddValidator(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(ApplicationAssembly, includeInternalTypes: true);

        // Our API follows the snake_case convention for responses, therefore it's recommended to maintain this
        // naming convention for validation responses.
        ValidatorOptions.Global.PropertyNameResolver = PropertyNameResolvers.SnakeCaseResolver;
    }
}