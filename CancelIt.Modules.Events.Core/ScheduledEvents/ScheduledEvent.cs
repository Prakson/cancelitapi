using CancelIt.Modules.Events.Core.ScheduledEvents.Events;
using CancelIt.Modules.Events.Core.ScheduledEvents.Exceptions;
using CancelIt.Modules.Events.Core.Shared;

namespace CancelIt.Modules.Events.Core.ScheduledEvents;

public class ScheduledEvent : AggregateRoot
{
    public TimeRange TimeRange { get; private set; }
    public string EventName { get; private set; }
    public ScheduledEventStatus Status => !IsCancelled() ? ScheduledEventStatus.Scheduled : ScheduledEventStatus.Cancelled;
    public IReadOnlyCollection<Participation> Participants => _participants.AsReadOnly();

    private readonly List<Participation> _participants = new();

    public ScheduledEvent(string eventName, TimeRange timeRange)
    {
        TimeRange = timeRange;
        EventName = eventName;
        
        AddDomainEvent(new EventScheduled(this));
    }

    public void Join(ParticipantId participantIdentity, DateTimeOffset currentTime)
    {
        EnsureEventIsNotCancelled();
        EnsureUserDoesNotParticipate(participantIdentity);
        
        _participants.Add(new Participation(participantIdentity, currentTime));
        
        AddDomainEvent(new ParticipantJoined(participantIdentity, this));
    }

    public void RequestCancellation(ParticipantId participantIdentity, DateTimeOffset currentTime)
    {
        EnsureUserParticipates(participantIdentity);
        
        var participation = _participants.First(x => x.ParticipantId == participantIdentity);
        if (participation.Cancelled)
        {
            throw new ParticipantAlreadyCancelled(participantIdentity);
        }
        
        participation.Cancel(currentTime);

        if (IsCancelled())
        {
            AddDomainEvent(new EventCancelled(this, participantIdentity));    
        }
    }

    private void EnsureUserParticipates(ParticipantId participantIdentity)
    {
        if (_participants.All(x => x.ParticipantId != participantIdentity))
        {
            throw new DoesNotParticipate(participantIdentity);
        }
    }

    private void EnsureUserDoesNotParticipate(ParticipantId participantIdentity)
    {
        if (_participants.Any(x => x.ParticipantId == participantIdentity))
        {
            throw new AlreadyParticipates(participantIdentity);
        }
    }

    private void EnsureEventIsNotCancelled()
    {
        if (IsCancelled())
        {
            throw new EventAlreadyCancelled();
        }
    }

    private bool IsCancelled() => _participants.Count > 0 && _participants.All(x => x.Cancelled);
}