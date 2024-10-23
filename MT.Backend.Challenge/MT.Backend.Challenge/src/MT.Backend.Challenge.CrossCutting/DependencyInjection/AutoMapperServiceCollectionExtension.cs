using MT.Backend.Challenge.Application.Mapping;
using Microsoft.Extensions.DependencyInjection;

namespace MT.Backend.Challenge.CrossCutting.DependencyInjection
{
    public static class AutoMapperServiceCollectionExtension
    {
        public static IServiceCollection AddAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingConfig));

            return services;
        }
    }
}
