namespace CancelIt.Modules.Events.Core.ScheduledEvents.Exceptions;

public class ParticipantAlreadyCancelled(ParticipantId participantId) : Exception("Participant already cancelled the event.")
{
    public ParticipantId ParticipantId { get; } = participantId;
}