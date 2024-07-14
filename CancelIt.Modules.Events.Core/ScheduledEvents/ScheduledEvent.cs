using CancelIt.Modules.Events.Core.ScheduledEvents.Events;
using CancelIt.Modules.Events.Core.Shared;

namespace CancelIt.Modules.Events.Core.ScheduledEvents;

public class ScheduledEvent : AggregateRoot
{
    public TimeRange TimeRange { get; private set; }
    public string HostIdentity { get; }
    public string EventName { get; private set; }

    public IReadOnlyCollection<string> Participants => _participants.AsReadOnly();

    private readonly List<string> _participants = new();

    public ScheduledEvent(string hostIdentity, string eventName, TimeRange timeRange)
    {
        TimeRange = timeRange;
        HostIdentity = hostIdentity;
        EventName = eventName;
        
        AddDomainEvent(new EventScheduled(this));
    }

    public void Join(string participantIdentity)
    {
        _participants.Add(participantIdentity);
        AddDomainEvent(new ParticipantJoined(participantIdentity, this));
    }
    
    public void RequestCancellation(string participantIdentity)
    {
        
    }
}