using MotorcycleRental.Domain.Events;
using System.Reflection;

namespace MotorcycleRental.Domain.Entities
{
    public abstract class AggregateRoot : IEntityBase
    {
        private readonly List<IDomainEvent> _events = new List<IDomainEvent>();
        public Guid Id { get; protected set; }
        public DateTime CreatedAt { get; protected set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsActived { get; protected set; }
        public IEnumerable<IDomainEvent> Events => _events;
        protected void AddEvent(IDomainEvent @event)
        {
            _events.Add(@event);
        }

        public void SetUpdateDate()
        {
            UpdatedAt = DateTime.UtcNow;
        }
        public void SetActive()
        {
            IsActived = true;
        }
        public void SetInactive()
        {
            IsActived = false;
        }
    }
}
