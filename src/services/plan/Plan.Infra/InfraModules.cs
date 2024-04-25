using BusConnections.Events;
using BusConnections.Events.Producer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Plan.Infra.Context;
using Plan.Infra.interfaces;
using Plan.Infra.Messaging;
using Plan.Infra.Repositories;
using RabbitMQ.Client;

namespace Plan.Infra;

public static class InfraModules
{
    internal static IConfiguration _configuration;

    public static IServiceCollection AddInfraModules(this IServiceCollection services)
    {
        services.AddMessageBus();
        services.AddAdbContext();
        services.AddMessageBusServicesConfig();
        return services;
    }
    private static IServiceCollection AddMessageBus(this IServiceCollection services)
    {
        services.AddScoped<IPlanRepository, PlanRepository>();
        return services;
    }
    private static IServiceCollection AddAdbContext(this IServiceCollection services)
    {
        var connectionString = "Host=localhost;Username=postgres;Password=postgres;Database=SampleDbDriver; PORT=5433";
        services.AddDbContextFactory<PlanContext>(options => options.UseNpgsql(connectionString));
        return services;
    }

    private static IServiceCollection AddMessageBusServicesConfig(this IServiceCollection services)
    {
        services.AddScoped<ProducerEvent>();
        services.AddSingleton<MbClient>();
        services.AddSingleton<PlanRpcServer>();
        return services;
    }

}