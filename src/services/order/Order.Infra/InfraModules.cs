using BusConnections.Events.Producer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Order.Infra.Context;
using Order.Infra.Interfaces;
using Order.Infra.Messaging;
using Order.Infra.Repositories;

namespace Order.Infra;

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
        services.AddScoped<IOrderRepository, OrderRepository>();
        var connectionString = "Host=localhost;Username=postgres;Password=postgres;Database=SampleDbDriver; PORT=5433";
        services.AddDbContextFactory<OrderContext>(options => options.UseNpgsql(connectionString));
        return services;
    }

    private static IServiceCollection AddMessageBusServicesConfig(this IServiceCollection services)
    {
        services.AddScoped<ProducerEvent>();
        services.AddSingleton<MbClient>();
        services.AddSingleton<OrderRpcServer>();
        return services;
    }
}