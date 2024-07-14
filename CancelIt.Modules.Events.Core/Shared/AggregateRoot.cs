namespace CancelIt.Modules.Events.Core.Shared;

public abstract class AggregateRoot
{
    public IReadOnlyCollection<DomainEvent> DomainEvents => _domainEvents.AsReadOnly();
    
    private readonly List<DomainEvent> _domainEvents = new();

    protected void AddDomainEvent(DomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }
    
    public void ClearDomainEvents()
    {
        _domainEvents?.Clear();
    }
}