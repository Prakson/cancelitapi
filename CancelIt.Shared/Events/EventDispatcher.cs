namespace CancelIt.Shared.Events;

public interface EventDispatcher
{
    // Task PublishAsync(IEvent @event, CancellationToken cancellationToken = default);
        
    Task PublishAsync<TEvent>(TEvent @event, CancellationToken cancellationToken = default)
        where TEvent : class, Event;
}