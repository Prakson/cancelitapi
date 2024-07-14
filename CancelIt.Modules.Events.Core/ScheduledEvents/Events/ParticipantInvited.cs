using CancelIt.Modules.Events.Core.Aggregates;
using MediatR;

namespace CancelIt.Modules.Events.Core.Events;

public record ParticipantInvited(string ParticipantIdentity, ScheduledEvent Event) : INotification
{
    
}