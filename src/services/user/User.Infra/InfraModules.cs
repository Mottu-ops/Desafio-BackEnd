using BusConnections.Events;
using BusConnections.Events.Producer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using User.Infra.context;
using User.Infra.interfaces;
using User.Infra.Messaging;
using User.Infra.Repositories;

namespace User.Infra;
public static class InfraModules {
    public static IServiceCollection AddInfraModules(this IServiceCollection services) {
        services.AddMessageBus();
        services.AddMessageBusServicesConfig();
        return services;
    }
    private static IServiceCollection AddMessageBus(this IServiceCollection services) {
        services.AddScoped<IUserRepository, UserRepository>();
        var connectionString = "Host=localhost;Username=postgres;Password=postgres;Database=SampleDbDriver; PORT=5433";
        services.AddDbContextFactory<UserContext>(options => options.UseNpgsql(connectionString));
        return services;
    }
    private static IServiceCollection AddMessageBusServicesConfig(this IServiceCollection services)
    {
        services.AddScoped<ProducerEvent>();
        services.AddSingleton<UserRpcServer>();
        return services;
    }
    
}