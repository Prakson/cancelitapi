using Microsoft.Extensions.DependencyInjection;

namespace CancelIt.Shared.Events;

internal sealed class ServiceProviderEventDispatcher(IServiceProvider serviceProvider) : EventDispatcher
{
    public async Task PublishAsync<TEvent>(TEvent @event, CancellationToken cancellationToken = default)
        where TEvent : class, Event
    {
        if (typeof(TEvent) == typeof(Event))
        {
            await DispatchDynamicallyAsync(@event, cancellationToken);
            return;
        }

        using var scope = serviceProvider.CreateScope();
        var handlers = scope.ServiceProvider.GetServices<EventHandler<TEvent>>();
        var tasks = handlers.Select(x => x.HandleAsync(@event, cancellationToken));
        await Task.WhenAll(tasks);
    }

    private async Task DispatchDynamicallyAsync(Event @event, CancellationToken cancellationToken = default)
    {
        using var scope = serviceProvider.CreateScope();
        var handlerType = typeof(EventHandler<>).MakeGenericType(@event.GetType());
        var handlers = scope.ServiceProvider.GetServices(handlerType);
        var method = handlerType.GetMethod(nameof(EventHandler<Event>.HandleAsync));
        if (method is null)
        {
            throw new InvalidOperationException($"Event handler for '{@event.GetType().Name}' is invalid.");
        }

        var tasks = handlers.Select(x => (Task)method.Invoke(x, new object[] { @event, cancellationToken }));
        await Task.WhenAll(tasks);
    }
}