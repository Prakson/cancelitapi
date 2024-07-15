using Microsoft.Extensions.DependencyInjection;

namespace CancelIt.Shared.Events;

internal static class Extensions
{
    public static IServiceCollection AddEvents(this IServiceCollection services)
    {
        services.AddSingleton<EventDispatcher, ServiceProviderEventDispatcher>();
        services.Scan(s => s.FromAssemblies(AppDomain.CurrentDomain.GetAssemblies())
            .AddClasses(c => c.AssignableTo(typeof(EventHandler<>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());
            
        return services;
    }
}