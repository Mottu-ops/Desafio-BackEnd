using RabbitMQ.Client;
using rent_core_api.Model;
using System.Text.Json;
using System.Text;

namespace rent_core_api.RabbitMQ
{
    public class RabbitMqPublisher
    {
        private readonly string _hostname = "localhost";
        private readonly string _queueName = "moto_events";
        private IConnection _connection;
        private readonly ILogger<RabbitMqPublisher> _logger;


        public RabbitMqPublisher(ILogger<RabbitMqPublisher> logger)
        {
            _logger = logger;
            CreateConnection();
        }
        private void CreateConnection()
        {
            try {
                var factory = new ConnectionFactory { HostName = _hostname };
                _connection = factory.CreateConnection();
            }catch (Exception ex){
                _logger.LogError(ex, "Failed to create RabbitMQ connection");
            }
        }

        public void PublishMotoEvent(Moto moto)
        {
            if (_connection == null)
            {
                CreateConnection();
            }

            using (var channel = _connection.CreateModel())
            {
                channel.QueueDeclare(queue: _queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

                var message = JsonSerializer.Serialize(moto);
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "", routingKey: _queueName, basicProperties: null, body: body);
            }
        }
    }
}
