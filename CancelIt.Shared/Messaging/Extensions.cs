using Microsoft.Extensions.DependencyInjection;

namespace CancelIt.Shared.Messaging;

internal static class Extensions
{
    private const string SectionName = "messaging";
        
    public static IServiceCollection AddMessaging(this IServiceCollection services)
    {
        services.AddTransient<MessageBroker, InMemoryMessageBroker>();
        services.AddTransient<AsyncEventDispatcher, ChannelAsyncEventDispatcher>();
        services.AddSingleton<EventChannel>();
        services.AddHostedService<EventDispatcherJob>();
            
        return services;
    }
}