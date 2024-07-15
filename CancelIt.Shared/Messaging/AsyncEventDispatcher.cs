using CancelIt.Shared.Events;

namespace CancelIt.Shared.Messaging;

internal interface AsyncEventDispatcher
{
    Task PublishAsync<TEvent>(TEvent @event, CancellationToken cancellationToken = default)
        where TEvent : class, Event;
}