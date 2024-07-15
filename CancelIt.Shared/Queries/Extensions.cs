using Microsoft.Extensions.DependencyInjection;

namespace CancelIt.Shared.Queries;

internal static class Extensions
{
    public static IServiceCollection AddQueries(this IServiceCollection services)
    {
        services.AddSingleton<QueryDispatcher, ServiceProviderQueryDispatcher>();
        services.Scan(s => s.FromAssemblies(AppDomain.CurrentDomain.GetAssemblies())
            .AddClasses(c => c.AssignableTo(typeof(QueryHandler<,>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());
            
        return services;
    }
}