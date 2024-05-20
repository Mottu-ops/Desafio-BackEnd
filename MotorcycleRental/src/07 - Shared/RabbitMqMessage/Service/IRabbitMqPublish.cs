namespace RabbitMqMessage.Service
{
    public interface IRabbitMqPublish
    {
        void Publish(object message, string routingKey, string queue, string exchange);
    }
}
