using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using rent_core_api.Model;
using System.Text;
using System.Text.Json;
using rent_core_api.Repository;

namespace rent_core_api.RabbitMQ
{
    public class RabbitMqConsumer
    {
        private readonly string _hostname = "localhost";
        private readonly string _queueName = "moto_events";
        private readonly MotoRepository _motoRepository;

        public RabbitMqConsumer(MotoRepository motoRepository)
        {
            _motoRepository = motoRepository ?? throw new ArgumentNullException(nameof(motoRepository));
        }

        public void StartConsuming()
        {
            var factory = new ConnectionFactory() { HostName = _hostname };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare(queue: _queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var moto = JsonSerializer.Deserialize<Moto>(message);

                if (moto != null && moto.ano == 2024)
                {
                    string msg = "Evento(2024) de moto cadastrada id" + moto.id;
                    var motoEvents = new MotoEvents(msg, moto.placa);
                    _motoRepository.AddCurrentYear(motoEvents);
                }
            };

            channel.BasicConsume(queue: _queueName, autoAck: true, consumer: consumer);
        }

    }
}
