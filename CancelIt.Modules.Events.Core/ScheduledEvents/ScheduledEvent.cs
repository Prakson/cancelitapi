using CancelIt.Modules.Events.Core.Aggregates;
using CancelIt.Modules.Events.Core.Events;

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
    
    public void Reschedule(DateTimeOffset startTime, DateTimeOffset endTime)
    {
        
    }

    public void Invite(string participantIdentity)
    {
        _participants.Add(participantIdentity);
        AddDomainEvent(new ParticipantInvited(participantIdentity, this));
    }
    
    public void Cancel(string participantIdentity)
    {
        
    }
}