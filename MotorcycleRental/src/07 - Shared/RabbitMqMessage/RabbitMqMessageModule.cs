using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMqMessage.Service;

namespace RabbitMqMessage
{
    public static class RabbitMqMessageModule
    {
        public static IServiceCollection AddRabbitMqMessage(this IServiceCollection services)
        {
            services.AddRabbitMqConfiguration();
            return services;
        }

        private static IServiceCollection AddRabbitMqConfiguration(this IServiceCollection services)
        {
            services.AddSingleton<IRabbitMqPublish, RabbitMqPublish>();
            return services;
        }
    }
}
