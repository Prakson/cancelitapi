using CancelIt.Shared.Events;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CancelIt.Shared.Messaging;

internal sealed class EventDispatcherJob(
    EventChannel eventChannel,
    EventDispatcher eventDispatcher,
    ILogger<EventDispatcherJob> logger)
    : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await foreach (var @event in eventChannel.Reader.ReadAllAsync(stoppingToken))
        {
            try
            {
                await eventDispatcher.PublishAsync(@event, stoppingToken);
            }
            catch (Exception exception)
            {
                logger.LogError(exception, exception.Message);
            }
        }
    }
}