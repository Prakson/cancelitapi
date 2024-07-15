using Microsoft.Extensions.DependencyInjection;

namespace CancelIt.Shared.Commands;

internal sealed class ServiceProviderCommandDispatcher(IServiceProvider serviceProvider) : CommandDispatcher
{
    public async Task SendAsync<TCommand>(TCommand command, CancellationToken cancellationToken = default) where TCommand : class, Command
    {
        if (command is null)
        {
            return;
        }

        using var scope = serviceProvider.CreateScope();
        var handler = scope.ServiceProvider.GetRequiredService<CommandHandler<TCommand>>();
        await handler.HandleAsync(command, cancellationToken);
    }
}