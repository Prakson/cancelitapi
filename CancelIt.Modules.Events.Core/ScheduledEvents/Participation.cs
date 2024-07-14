namespace CancelIt.Modules.Events.Core.ScheduledEvents;

public class Participation(ParticipantId participantId, DateTimeOffset joinedAt)
{
    public ParticipantId ParticipantId { get; }= participantId;
    public DateTimeOffset JoinedAt { get; } = joinedAt;
    public DateTimeOffset? CancelledAt { get; private set; }
    public bool Cancelled => CancelledAt is not null;
    public void Cancel(DateTimeOffset currentTime) => CancelledAt = currentTime;
}