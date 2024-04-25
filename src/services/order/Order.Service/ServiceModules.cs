using Microsoft.Extensions.DependencyInjection;
using Order.Service.Interfaces;
using Order.Service.Profiles;
using Order.Service.Services;


namespace Order.Service;
public static class ServiceModules {
    public static IServiceCollection AddServiceModules(this IServiceCollection services) {
        services.AddAutoMappersServices();
        services.AddServiceScoped();
        return services;
    }
    private static IServiceCollection AddAutoMappersServices(this IServiceCollection services) {
        services.AddAutoMapper(typeof(OrderAutoMapperProfile));
        return services;
    }
    private static IServiceCollection AddServiceScoped(this IServiceCollection services) {
        services.AddScoped<IOrderServices, OrderService>();
        return services;
    }
}