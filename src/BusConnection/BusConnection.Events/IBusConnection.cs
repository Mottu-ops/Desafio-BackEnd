using RabbitMQ.Client;

namespace BusConnections.Events
{
    public interface IBusConnection : IDisposable
    {
        bool IsConnected { get; }
        bool TryConnect();
        IModel CreateModel();
    }
}
