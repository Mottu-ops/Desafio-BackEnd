using Microsoft.Extensions.DependencyInjection;
using System;

namespace MT.Backend.Challenge.CrossCutting.DependencyInjection
{
    public static class MediatorServiceCollectionExtension
    {
        public static IServiceCollection AddMediator(this IServiceCollection services)
        {
            var assembly = AppDomain.CurrentDomain.Load("MT.Backend.Challenge.Application");
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly));

            return services;
        }
    }
}
