using MediatR;

namespace CancelIt.Modules.Events.Core.Aggregates;

public abstract class AggregateRoot
{
    public IReadOnlyCollection<INotification> DomainEvents => _domainEvents.AsReadOnly();
    
    private readonly List<INotification> _domainEvents = new();

    protected void AddDomainEvent(INotification domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }
    
    public void ClearDomainEvents()
    {
        _domainEvents?.Clear();
    }
}