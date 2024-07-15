using CancelIt.Shared.Commands;
using CancelIt.Shared.Events;
using CancelIt.Shared.Queries;

namespace CancelIt.Shared.Dispatchers;

internal sealed class InMemoryDispatcher(
    CommandDispatcher commandDispatcher,
    EventDispatcher eventDispatcher,
    QueryDispatcher queryDispatcher)
    : Dispatcher
{
    public Task SendAsync<T>(T command, CancellationToken cancellationToken = default) where T : class, Command
        => commandDispatcher.SendAsync(command, cancellationToken);

    public Task PublishAsync<T>(T @event, CancellationToken cancellationToken = default) where T : class, Event
        => eventDispatcher.PublishAsync(@event, cancellationToken);

    public Task<TResult> QueryAsync<TResult>(Query<TResult> query, CancellationToken cancellationToken = default)
        => queryDispatcher.QueryAsync(query, cancellationToken);
}