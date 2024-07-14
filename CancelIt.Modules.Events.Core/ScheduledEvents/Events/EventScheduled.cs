using CancelIt.Modules.Events.Core.Aggregates;
using MediatR;

namespace CancelIt.Modules.Events.Core.Events;

public record EventScheduled(ScheduledEvent ScheduledEvent) : INotification
{
    
}