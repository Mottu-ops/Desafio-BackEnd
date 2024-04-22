using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Motorent.Application.Common.Behaviors;
using Motorent.Application.Common.Mappings;
using Motorent.Application.Common.Validations;

namespace Motorent.Application;

public static class ServiceExtensions
{
    private static readonly Assembly ApplicationAssembly = typeof(ServiceExtensions).Assembly;

    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMapper();

        services.AddMediator();

        services.AddValidator();

        RegisterAuthorizationServices(services);

        return services;
    }

    private static void AddMapper(this IServiceCollection services)
    {
        var config = TypeAdapterConfig.GlobalSettings;
        config.Scan(ApplicationAssembly);

        // This will automatically trim all string properties when mapping from source to destination.
        config.Default.AddDestinationTransform(StringTransformFunctions.Trim);

        TypeAdapterConfig.GlobalSettings.AllowImplicitDestinationInheritance = true;

        services.AddSingleton(config);
    }

    private static void AddMediator(this IServiceCollection services) => services.AddMediatR(config =>
    {
        config.RegisterServicesFromAssembly(ApplicationAssembly);

        config.AddOpenBehavior(typeof(LoggingBehavior<,>))
            .AddOpenBehavior(typeof(ExceptionBehavior<,>))
            .AddOpenBehavior(typeof(AuthorizationBehavior<,>))
            .AddOpenBehavior(typeof(ValidationBehavior<,>))
            .AddOpenBehavior(typeof(TransactionBehavior<,>));
    });

    private static void AddValidator(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(ApplicationAssembly, includeInternalTypes: true);

        // Our API follows the snake_case convention for responses, therefore it's recommended to maintain this
        // naming convention for validation responses.
        ValidatorOptions.Global.PropertyNameResolver = PropertyNameResolvers.SnakeCaseResolver;
    }
    
    private static void RegisterAuthorizationServices(IServiceCollection services)
    {
        RegisterGenericTypes(services, typeof(IAuthorizer<>), ServiceLifetime.Scoped);
        RegisterGenericTypes(services, typeof(IAuthorizationRequirementHandler<>), ServiceLifetime.Scoped);
    }
    
    private static void RegisterGenericTypes(this IServiceCollection services, Type genericType,
        ServiceLifetime lifetime)
    {
        var implementationTypes = GetTypesImplementingGenericType(ApplicationAssembly, genericType);
        foreach (var implementationType in implementationTypes)
        {
            foreach (var interfaceType in implementationType.ImplementedInterfaces)
            {
                if (interfaceType.IsGenericType && interfaceType.GetGenericTypeDefinition() == genericType)
                {
                    services.Add(new ServiceDescriptor(interfaceType, implementationType, lifetime));
                }
            }
        }
    }

    private static List<TypeInfo> GetTypesImplementingGenericType(Assembly assembly, Type genericType)
    {
        if (!genericType.IsGenericType)
        {
            throw new ArgumentException("Must be a generic type", nameof(genericType));
        }

        return assembly.DefinedTypes
            .Where(t => t is { IsClass: true, IsAbstract: false } && t.GetInterfaces()
                .Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == genericType))
            .ToList();
    }
}