using Microsoft.Extensions.DependencyInjection;

namespace CancelIt.Shared.Queries;

internal sealed class ServiceProviderQueryDispatcher(IServiceProvider serviceProvider) : QueryDispatcher
{
    public async Task<TResult> QueryAsync<TResult>(Query<TResult> query, CancellationToken cancellationToken = default)
    {
        using var scope = serviceProvider.CreateScope();
        var handlerType = typeof(QueryHandler<,>).MakeGenericType(query.GetType(), typeof(TResult));
        var handler = scope.ServiceProvider.GetRequiredService(handlerType);
        var method = handlerType.GetMethod(nameof(QueryHandler<Query<TResult>, TResult>.HandleAsync));
        if (method is null)
        {
            throw new InvalidOperationException($"Query handler for '{typeof(TResult).Name}' is invalid.");
        }

        // ReSharper disable once PossibleNullReferenceException
        return await (Task<TResult>)method.Invoke(handler, new object[] {query, cancellationToken});
    }
}