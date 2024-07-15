namespace CancelIt.Shared.Events;

public interface EventHandler<in TEvent> where TEvent : class, Event
{
    Task HandleAsync(TEvent @event, CancellationToken cancellationToken = default);
}