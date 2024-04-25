using System.Collections.Concurrent;
using System.Text;
using BusConnections.Core.Model;
using BusConnections.Events.Core;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace BusConnections.Events.Producer
{
    public class MbClient
    {
        private readonly IModel _channel;
        private readonly string _replyQueueName;
        private readonly EventingBasicConsumer _consumer;
        private readonly BlockingCollection<string> _respQueue = new BlockingCollection<string>();
        private readonly IBasicProperties _props;
        private readonly IBusConnection _persistentConnection;

        public MbClient(IBusConnection persistentConnection)
        {
            _persistentConnection = persistentConnection;

            if (!persistentConnection.IsConnected)
                persistentConnection.TryConnect();

            _channel = persistentConnection.CreateModel();
            _channel.ConfirmSelect();
            _replyQueueName = $"{ EventBusConstants.RdcReplyQueue}";

            var args = new Dictionary<string, object>();
            args.Add("x-single-active-consumer", true);

            _channel.QueueDeclare(queue: _replyQueueName, durable: false,
                  exclusive: false, autoDelete: false, arguments: args);
            _consumer = new EventingBasicConsumer(_channel);

            _props = _channel.CreateBasicProperties();
            var correlationId = Guid.NewGuid().ToString();
            _props.CorrelationId = correlationId;
            _props.ReplyTo = _replyQueueName;

            _consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var response = Encoding.UTF8.GetString(body);
                if (ea.BasicProperties.CorrelationId == correlationId)
                {
                    _respQueue.Add(response);
                }
            };
            _channel.BasicAcks += (sender, ea) =>
            {

            };
            _channel.BasicNacks += (sender, ea) =>
            {

            };
        }

        public Response? Call(Request obj)
        {
            var message = JsonConvert.SerializeObject(obj);
            var body = Encoding.UTF8.GetBytes(message);

            _channel.BasicPublish(
               exchange: "",
               routingKey: $"{ EventBusConstants.RdcPublishQueue}",
               basicProperties: _props,
               body: body);

            _channel.BasicConsume(
               consumer: _consumer,
               queue: _replyQueueName,
               autoAck: true);

            var receivedMessage = _respQueue.Take();

            return JsonConvert.DeserializeObject<Response>(receivedMessage);
        }

        public void Close()
        {
            _persistentConnection.Dispose();
        }
    }
}
