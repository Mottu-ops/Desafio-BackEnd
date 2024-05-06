using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using User.Infra.Context;
using User.Infra.Interfaces;
using User.Infra.Repositories;

namespace User.Infra
{
    public static class InfraModulesExtensions
    {
        public static IServiceCollection AddInfraModules(this IServiceCollection services)
        {
            services.AddDatabase();
            return services;
        }

        public static IServiceCollection AddDatabase(this IServiceCollection services)
        {
            services.AddDbContextFactory<UserContext>();

            services.AddScoped<IUserRepository, UserRepository>();
            return services;
        }
    }
}
