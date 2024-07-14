namespace CancelIt.Modules.Events.Core.ScheduledEvents.Exceptions;

public class DoesNotParticipate(ParticipantId participantId) : Exception("Participant does not participate in the event.")
{
    public ParticipantId ParticipantId { get; } = participantId;
}