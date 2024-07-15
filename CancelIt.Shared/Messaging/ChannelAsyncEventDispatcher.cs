using CancelIt.Shared.Events;

namespace CancelIt.Shared.Messaging;

internal sealed class ChannelAsyncEventDispatcher(EventChannel channel) : AsyncEventDispatcher
{
    public async Task PublishAsync<TEvent>(TEvent @event, CancellationToken cancellationToken = default)
        where TEvent : class, Event
    {
        await channel.Writer.WriteAsync(@event, cancellationToken);
    }
}