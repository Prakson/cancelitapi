using CancelIt.Modules.Events.Core.Shared;

namespace CancelIt.Modules.Events.Core.ScheduledEvents.Events;

public record ParticipantJoined(string ParticipantIdentity, ScheduledEvent Event) : DomainEvent
{
    
}