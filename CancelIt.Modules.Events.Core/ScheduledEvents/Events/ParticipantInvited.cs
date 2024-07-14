using CancelIt.Modules.Events.Core.Aggregates;
using CancelIt.Modules.Events.Core.ScheduledEvents;
using MediatR;

namespace CancelIt.Modules.Events.Core.Events;

public record ParticipantInvited(string ParticipantIdentity, ScheduledEvent Event) : INotification
{
    
}