using CancelIt.Shared.Commands;
using CancelIt.Shared.Events;
using CancelIt.Shared.Queries;

namespace CancelIt.Shared.Dispatchers;

public interface Dispatcher
{
    Task SendAsync<T>(T command, CancellationToken cancellationToken = default) where T : class, Command;
    Task PublishAsync<T>(T @event, CancellationToken cancellationToken = default) where T : class, Event;
    Task<TResult> QueryAsync<TResult>(Query<TResult> query, CancellationToken cancellationToken = default);
}