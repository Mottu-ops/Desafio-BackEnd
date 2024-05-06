
using Microsoft.Extensions.DependencyInjection;
using User.Infra.Repositories;
using User.Infra.Interfaces;
using User.Services.Interfaces;
using User.Services.Service;
using Microsoft.Extensions.Configuration;

namespace User.Services
{
    public static class ServiceModulesExtensions
    {
        public static IServiceCollection AddServicesModules(this IServiceCollection services)
        {
            services.AddServiceScoped();
            services.AddServiceRedis();
            return services;
        }

        private static IServiceCollection AddServiceScoped(this IServiceCollection services)
        {
            services.AddScoped<IClientServices, ClientServices>();
            services.AddScoped<IUserRepository, UserRepository>();


            services.AddScoped<IClientImageUploadService, ClientImageUploadService>();


            return services;
        }

        private static IServiceCollection AddServiceRedis(this IServiceCollection services)
        {
            services.AddScoped<IRedisService, RedisService>(p =>
            {
                var configuration = p.GetRequiredService<IConfiguration>();
                var conn = configuration["Redis:ConnectionString"];

                return new RedisService(conn!);
            });
            return services;
        }

    }
}
