namespace CancelIt.Shared.Queries;

public interface QueryHandler<in TQuery, TResult> where TQuery : class, Query<TResult>
{
    Task<TResult> HandleAsync(TQuery query, CancellationToken cancellationToken = default);
}