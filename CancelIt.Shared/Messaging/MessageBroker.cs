using CancelIt.Shared.Events;

namespace CancelIt.Shared.Messaging;

public interface MessageBroker
{
    Task PublishAsync(Event @event, CancellationToken cancellationToken = default);
}