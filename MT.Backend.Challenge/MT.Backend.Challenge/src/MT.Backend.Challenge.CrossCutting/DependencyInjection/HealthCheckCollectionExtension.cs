using MT.Backend.Challenge.CrossCutting.Health;
using Microsoft.Extensions.DependencyInjection;

namespace MT.Backend.Challenge.CrossCutting.DependencyInjection
{
    public static class HealthCheckCollectionExtension
    {
        public static IServiceCollection AddHealthChecksInjection(this IServiceCollection services)
        {
            services.AddHealthChecks()
                .AddCheck<ApiHealthCheck>("Api_Healthy");
            return services;
        }
    }
}
