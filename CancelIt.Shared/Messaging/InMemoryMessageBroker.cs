using CancelIt.Shared.Events;
using Humanizer;
using Microsoft.Extensions.Logging;

namespace CancelIt.Shared.Messaging;

internal sealed class InMemoryMessageBroker(
    AsyncEventDispatcher asyncEventDispatcher,
    ILogger<InMemoryMessageBroker> logger)
    : MessageBroker
{
    public async Task PublishAsync(Event @event, CancellationToken cancellationToken = default)
    {
        var name = @event.GetType().Name.Underscore();
        logger.LogInformation("Publishing an event: {Name}...", name);
        await asyncEventDispatcher.PublishAsync(@event, cancellationToken);
    }
}