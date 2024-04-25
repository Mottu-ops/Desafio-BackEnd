using BusConnections.Events.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Motorcycle.Infra.Messaging.Consumers;

namespace Motorcycle.Infra.Messaging.Extensions
{
    public static class MessageBrokerExtensions
    {
        public static IServiceProvider _serviceProvider;
        public static MotorcycleRpcServer _listener { get;set; }

        public static IApplicationBuilder UseRabbitListener(this IApplicationBuilder app, IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            var life = app.ApplicationServices.GetService<IHostApplicationLifetime>();

            life?.ApplicationStarted.Register(OnStarted);
            life?.ApplicationStopped.Register(OnStopping);
            _listener = app.ApplicationServices.GetService<MotorcycleRpcServer>();

            return app;
        }

        private static void OnStarted()
        {
            ActivatorUtilities.CreateInstance<MotorcycleBusConsumer>(_serviceProvider).Consume($"{EventBusConstants.DirectQueue}");
            _listener.Consume($"{EventBusConstants.RdcPublishQueue}");
        }
        private static void OnStopping()
        {

        }
    }
}
