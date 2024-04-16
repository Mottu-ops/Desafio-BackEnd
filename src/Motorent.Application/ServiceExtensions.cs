using System.Reflection;
using FluentValidation;
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
            .AddOpenBehavior(typeof(TransactionBehavior<,>));
    });

    private static void AddValidator(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(ApplicationAssembly, includeInternalTypes: true);

        // Our API follows the snake_case convention for responses, therefore it's recommended to maintain this
        // naming convention for validation responses.
        ValidatorOptions.Global.PropertyNameResolver = PropertyNameResolvers.SnakeCaseResolver;
    }
}