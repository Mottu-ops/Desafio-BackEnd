using MotorcycleRental.Domain.Events;

namespace MotorcycleRental.Infraestructure.MessageBus
{
    public interface IEventProcessor
    {
        void Process(IEnumerable<IDomainEvent> events);
        void Process(IEnumerable<IDomainEvent> events, string? queue, string? exchange);
    }
}
