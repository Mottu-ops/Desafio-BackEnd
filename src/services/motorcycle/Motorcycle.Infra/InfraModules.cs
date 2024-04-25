using BusConnections.Events.Producer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Motorcycle.Infra.Context;
using Motorcycle.Infra.Interfaces;
using Motorcycle.Infra.Messaging;
using Motorcycle.Infra.Repositories;

namespace Motorcycle.Infra;

public static class InfraModules
{
    public static IServiceCollection AddInfraModules(this IServiceCollection services)
    {
        services.AddAdbContext();
        services.AddMessageBusServicesConfig();
        return services;
    }
    private static IServiceCollection AddAdbContext(this IServiceCollection services)
    {
        services.AddScoped<IMotorCycleRepository, MotorcycleRepository>();
        var connectionString = "Host=localhost;Username=postgres;Password=postgres;Database=SampleDbDriver; PORT=5433";
        services.AddDbContextFactory<MotorcycleContext>(options => options.UseNpgsql(connectionString));
        return services;
    }

    private static IServiceCollection AddMessageBusServicesConfig(this IServiceCollection services)
    {
        services.AddScoped<ProducerEvent>();
        services.AddSingleton<MbClient>();
        services.AddSingleton<MotorcycleRpcServer>();
        return services;
    }
}