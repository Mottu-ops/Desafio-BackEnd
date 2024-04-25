using System.Net.Sockets;
using System.Text;
using Polly;
using RabbitMQ.Client;
using RabbitMQ.Client.Exceptions;

namespace BusConnections.Events.Producer
{
    public class ProducerEvent
    {
        private readonly IBusConnection _persistentConnection;

        public ProducerEvent(IBusConnection persistentConnection)
        {
            _persistentConnection = persistentConnection;
        }

        public void Publish(string queueName, string message = null, int retryCount = 1)
        {
            if (!_persistentConnection.IsConnected)
            {
                try
                {
                    _persistentConnection.TryConnect();
                }
                catch (Exception ex)
                {
                    //logging
                    return;
                }
            }

            var policy = Policy.Handle<BrokerUnreachableException>()
                .Or<SocketException>()
                .WaitAndRetry(retryCount, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)), (ex, time) =>
                {
                    //logging
                });

            using var channel = _persistentConnection.CreateModel();
            var args = new Dictionary<string, object>();
            args.Add("x-single-active-consumer", true);

            channel.QueueDeclare(queueName, durable: true, exclusive: false, autoDelete: false, arguments: args);

            var body = Encoding.UTF8.GetBytes(message);

            policy.Execute(() =>
            {
                IBasicProperties properties = channel.CreateBasicProperties();
                properties.Persistent = true;
                properties.DeliveryMode = 2;

                channel.ConfirmSelect();
                channel.BasicPublish(
                    exchange: "",
                    routingKey: queueName,
                    mandatory: true,
                    basicProperties: properties,
                    body: body);
                channel.WaitForConfirmsOrDie();

                channel.BasicAcks += (sender, eventArgs) =>
                {
                    //Console.WriteLine("Sent RabbitMq");
                };
            });
        }
    }
}