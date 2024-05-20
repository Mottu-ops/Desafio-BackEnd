namespace MotorcycleRental.Infraestructure.MessageBus
{
    public interface IMessageBusClient
    {
        void Publish(object message, string routingKey, string queue, string exchange);
    }
}
