namespace CancelIt.Modules.Events.Core.ScheduledEvents.Exceptions;

public class AlreadyParticipates(ParticipantId participantId) : Exception("Participant already participates in the event.")
{
    public ParticipantId ParticipantId { get; } = participantId;
}