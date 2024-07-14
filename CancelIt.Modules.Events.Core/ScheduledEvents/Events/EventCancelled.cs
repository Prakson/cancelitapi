using CancelIt.Modules.Events.Core.Shared;

namespace CancelIt.Modules.Events.Core.ScheduledEvents.Events;

public record EventCancelled(ScheduledEvent Event, ParticipantId CausedBy) : DomainEvent
{
    
}