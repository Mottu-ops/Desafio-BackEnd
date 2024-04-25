using Microsoft.Extensions.DependencyInjection;
using User.Infra.interfaces;
using User.Infra.Repositories;
using User.Service.Bundles;
using User.Service.Interfaces;
using User.Service.Profiles;

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
        services.AddAutoMapper(typeof(UserAutoMapperProfile));
        return services;
    }
    private static IServiceCollection AddServiceScoped(this IServiceCollection services)
    {
        services.AddScoped<IPartnerServices, PartnerServices>();
        services.AddScoped<IUserRepository, UserRepository>();
        return services;
    }
}