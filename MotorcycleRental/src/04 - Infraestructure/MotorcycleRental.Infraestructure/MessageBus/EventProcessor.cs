using MotorcycleRental.Domain.Events;
using System.Text;

namespace MotorcycleRental.Infraestructure.MessageBus
{
    public class EventProcessor : IEventProcessor
    {
        private readonly IMessageBusClient _messageBusClient;

        public EventProcessor(IMessageBusClient messageBusClient)
        {
            _messageBusClient = messageBusClient;
        }

        public void Process(IEnumerable<IDomainEvent> events)
        {
            foreach (var @event in events)
            {
                var routerKey = ToDashCase(@event.GetType().Name);
                _messageBusClient.Publish(@event, routerKey, "year_2024", "motorcycle-service");
            }
        }

        public void Process(IEnumerable<IDomainEvent> events, string queue, string exchange)
        {
            foreach (var @event in events)
            {
                var routerKey = ToDashCase(@event.GetType().Name);
                _messageBusClient.Publish(@event, routerKey, queue, exchange);
            }
        }

        private static string ToDashCase(string input)
        {
            var sb = new StringBuilder();

            for (int i = 0; i < input.Length; i++)
            {
                if (i != 0 && char.IsUpper(input[i]))
                    sb.Append($"-{input[i]}");
                else
                    sb.Append(input[i]);
            }

            return sb.ToString().ToLower();
        }
    }
}
