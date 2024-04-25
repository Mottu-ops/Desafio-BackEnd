using Microsoft.Extensions.DependencyInjection;
using Plan.Service.Interfaces;
using Plan.Service.Profiles;
using Plan.Service.Services;


namespace Plan.Service;
public static class ServiceModules {
    public static IServiceCollection AddServiceModules(this IServiceCollection services) {
        services.AddAutoMappersServices();
        services.AddServiceScoped();
        return services;
    }
    private static IServiceCollection AddAutoMappersServices(this IServiceCollection services) {
        services.AddAutoMapper(typeof(PlanAutoMapperProfile));
        return services;
    }
    private static IServiceCollection AddServiceScoped(this IServiceCollection services) {
        services.AddScoped<IPlanServices, PlanService>();
        return services;
    }
}