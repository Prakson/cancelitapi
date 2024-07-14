using CancelIt.Modules.Events.Core.Events;

namespace CancelIt.Modules.Events.Core.Aggregates;

public class ScheduledEvent : AggregateRoot
{
    public string HostIdentity { get; }
    public string EventName { get; private set; }
    public DateTimeOffset StartTime { get; private set; }
    public DateTimeOffset EndTime { get; private set; }

    public IReadOnlyCollection<string> Participants => _participants.AsReadOnly();

    private readonly List<string> _participants = new();

    public ScheduledEvent(string hostIdentity, string eventName, DateTimeOffset startTime, DateTimeOffset endTime)
    {
        HostIdentity = hostIdentity;
        EventName = eventName;
        StartTime = startTime;
        EndTime = endTime;
        
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