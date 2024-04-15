using Motorent.Domain.Common.Events;

namespace Motorent.Domain.Common.Entities;

public interface IEntity
{
    IReadOnlyCollection<IEvent> Events { get; }
    
    void RaiseEvent(IEvent @event);
    
    void ClearEvents();
}

public interface IEntity<out TId> : IEntity where TId : notnull
{
    TId Id { get; }
}