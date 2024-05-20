using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using RabbitMQ.Client;
using System.Text;

namespace MotorcycleRental.Infraestructure.MessageBus
{
    public class RabbiMqClient : IMessageBusClient
    {
        public void Publish(object message, string routingKey, string queue, string exchange)
        {
            ConnectionFactory connectionFactory = new ConnectionFactory() { };
            using (var connection = connectionFactory.CreateConnection())
            {
                using (var model = connection.CreateModel())
                {
                    model.ExchangeDeclare(exchange, "topic", true, false);
                    model.QueueDeclare(queue, true, false, false);
                    model.QueueBind(queue, exchange, routingKey, null);

                    var settings = new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore,
                        ContractResolver = new CamelCasePropertyNamesContractResolver()
                    };

                    var payload = JsonConvert.SerializeObject(message, settings);

                    var body = Encoding.UTF8.GetBytes(payload);
                    model.BasicPublish(exchange, routingKey, null, body);
                }
            }
        }
    }
}
