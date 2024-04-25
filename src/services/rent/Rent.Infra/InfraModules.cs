
using BusConnections.Events.Producer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Rent.Infra.Context;
using Rent.Infra.Interfaces;
using Rent.Infra.Repositories;

namespace Rent.Infra;

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
        services.AddScoped<ITransactionRepository, TransactionRepository>();
        return services;
    }
    private static IServiceCollection AddAdbContext(this IServiceCollection services)
    {
        var connectionString = "Host=localhost;Username=postgres;Password=postgres;Database=SampleDbDriver; PORT=5433";
        services.AddDbContextFactory<TransactionContext>(options => options.UseNpgsql(connectionString));
        return services;
    }

    private static IServiceCollection AddMessageBusServicesConfig(this IServiceCollection services)
    {
        services.AddScoped<ProducerEvent>();
        services.AddSingleton<MbClient>();
        return services;
    }

}