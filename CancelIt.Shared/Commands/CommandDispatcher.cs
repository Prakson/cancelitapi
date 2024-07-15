namespace CancelIt.Shared.Commands;

public interface CommandDispatcher
{
    Task SendAsync<TCommand>(TCommand command, CancellationToken cancellationToken = default) where TCommand : class, Command;
}