using Microsoft.Extensions.DependencyInjection;
using Motorcycle.Service.Interfaces;
using Motorcycle.Service.Profiles;
using Motorcycle.Service.Services;


namespace Motorcycle.Service;
public static class ServiceModules {
    public static IServiceCollection AddServiceModules(this IServiceCollection services) {
        services.AddAutoMappersServices();
        services.AddServiceScoped();
        return services;
    }
    private static IServiceCollection AddAutoMappersServices(this IServiceCollection services) {
        services.AddAutoMapper(typeof(VehicleAutoMapperProfile));
        return services;
    }
    private static IServiceCollection AddServiceScoped(this IServiceCollection services) {
        services.AddScoped<IMotorcycleServices, MotorcycleService>();
        return services;
    }
}