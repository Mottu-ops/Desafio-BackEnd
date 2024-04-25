using RabbitMQ.Client;

namespace Motorcycle.Infra.Interfaces;

public interface IMessageBrokerPersistentConnection : IDisposable
{
    bool IsConnected { get; }
    bool TryConnect();
    IModel CreateModel();
}