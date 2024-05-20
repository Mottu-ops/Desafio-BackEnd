using RabbitMQ.Client;

namespace MotorcycleRental.Infraestructure.MessageBus
{
    public class ProducerConnection
    {
        public IConnection Connection { get; set; }

        public ProducerConnection(IConnection connection)
        {
            Connection = connection;
        }
    }
}
