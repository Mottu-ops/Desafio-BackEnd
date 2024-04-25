using BusConnections.Events.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using User.Infra.Messaging.Consumers;

namespace User.Infra.Messaging.Extensions
{
    public static class MessageBrokerExtensions
    {
        public static IServiceProvider _serviceProvider;
        public static UserRpcServer UserRpcListener { get;set; }

        public static IApplicationBuilder UseRabbitListener(this IApplicationBuilder app, IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            var life = app.ApplicationServices.GetService<IHostApplicationLifetime>();

            life?.ApplicationStarted.Register(OnStarted);
            life?.ApplicationStopped.Register(OnStopping);
            UserRpcListener = app.ApplicationServices.GetService<UserRpcServer>();

            return app;
        }

        private static void OnStarted()
        {
            ActivatorUtilities.CreateInstance<UserBusConsumer>(_serviceProvider).Consume($"{EventBusConstants.DirectQueue}");
            UserRpcListener.Consume($"{EventBusConstants.RdcPublishQueue}");
        }
        private static void OnStopping()
        {

        }
    }
}
