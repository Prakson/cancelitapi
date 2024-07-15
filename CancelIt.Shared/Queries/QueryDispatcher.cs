namespace CancelIt.Shared.Queries;

public interface QueryDispatcher
{
    Task<TResult> QueryAsync<TResult>(Query<TResult> query, CancellationToken cancellationToken = default);
}