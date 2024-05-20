using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Driver;
using MotorcycleRental.Domain.Interfaces;
using MotorcycleRental.Domain.Interfaces.Repositories.MongoDb;
using MotorcycleRental.Infraestructure.Context.MongoDb;
using MotorcycleRental.Infraestructure.Context.Postgress;
using MotorcycleRental.Infraestructure.MessageBus;
using MotorcycleRental.Infraestructure.Repositories;
using MotorcycleRental.Infraestructure.Repositories.MongoDb;

namespace MotorcycleRental.Infraestructure
{
    public static class InfraestructureModule
    {
        public static IServiceCollection AddInfraestructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName))
            );
            services.AddMongo();
            services.AddRepositories();
            services.AddRabbitMqConfiguration();
            return services;
        }
        public static IServiceCollection AddInfraestructureService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMongo();
            services.AddMongoRepositories();
            services.AddRabbitMqConfiguration();
            return services;
        }

        private static IServiceCollection AddRabbitMqConfiguration(this IServiceCollection services)
        {
            services.AddScoped<IMessageBusClient, RabbiMqClient>();
            services.AddScoped<IEventProcessor, EventProcessor>();
            return services;
        }

        private static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IMotorcycleRepository, MotorcycleRepository>();
            services.AddScoped<IRentalPlanRepository, RentalPlanRepository>();
            services.AddScoped<IDeliverymanRepository, DeliverymanRepository>();
            services.AddScoped<IRentalContractRepository, RentalContractRepository>();
            return services;
        }

        private static IServiceCollection AddMongoRepositories(this IServiceCollection services)
        {
            services.AddSingleton<IMotorcycleMongoRepository, MotorcycleMongoRepository>();
            return services;
        }

        private static IServiceCollection AddMongo(this IServiceCollection services)
        {
            services.AddSingleton(sp =>
            {
                var configuration = sp.GetService<IConfiguration>();
                var options = new MongoDbOptions();

                configuration.GetSection("Mongo").Bind(options);

                return options;
            });

            services.AddSingleton<IMongoClient>(sp =>
            {
                var options = sp.GetService<MongoDbOptions>();

                return new MongoClient(options.ConnectionString);
            });

            services.AddTransient(sp =>
            {
                BsonDefaults.GuidRepresentation = GuidRepresentation.Standard;

                var option = sp.GetService<MongoDbOptions>();
                var mongoClient = sp.GetService<IMongoClient>();

                return mongoClient.GetDatabase(option.Database);
            });

            return services;
        }

    }
}
