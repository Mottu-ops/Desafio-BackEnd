using MT.Backend.Challenge.Domain.Entities;
using MT.Backend.Challenge.Domain.Repositories;
using MT.Backend.Challenge.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace MT.Backend.Challenge.CrossCutting.DependencyInjection
{
    public static class RepositoryServiceCollectionExtension
    {
        public static IServiceCollection AddRepository(this IServiceCollection services)
        {
   
            services.AddScoped<IReadRepository<DeliveryDriver>, ReadRepository<DeliveryDriver>>();
            services.AddScoped<IWriteRepository<DeliveryDriver>, WriteRepository<DeliveryDriver>>();

            services.AddScoped<IReadRepository<Motorcycle>, ReadRepository<Motorcycle>>();
            services.AddScoped<IWriteRepository<Motorcycle>, WriteRepository<Motorcycle>>();

            services.AddScoped<IReadRepository<Rental>, ReadRepository<Rental>>();
            services.AddScoped<IWriteRepository<Rental>, WriteRepository<Rental>>();

            services.AddScoped<IReadRepository<RentalCategory>, ReadRepository<RentalCategory>>();
            services.AddScoped<IWriteRepository<RentalCategory>, WriteRepository<RentalCategory>>();

            return services;
        }
    }
}
