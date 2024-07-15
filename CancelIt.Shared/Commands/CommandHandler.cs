namespace CancelIt.Shared.Commands;

public interface CommandHandler<in TCommand> where TCommand : class, Command
{
    Task HandleAsync(TCommand command, CancellationToken cancellationToken = default);
}