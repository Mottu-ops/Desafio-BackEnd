using Microsoft.Extensions.DependencyInjection;
using Rent.Service.Interfaces;
using Rent.Service.Profiles;
using Rent.Service.Services;

namespace User.Service;
public static class ServiceModules
{
    public static IServiceCollection AddServiceModules(this IServiceCollection services)
    {
        services.AddAutoMappersServices();
        services.AddServiceScoped();
        return services;
    }
    private static IServiceCollection AddAutoMappersServices(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(TransactionProfile));
        return services;
    }
    private static IServiceCollection AddServiceScoped(this IServiceCollection services)
    {
        services.AddScoped<IRentService, RentService>();
        return services;
    }
}