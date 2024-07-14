namespace CancelIt.Modules.Events.Core.ScheduledEvents;

public readonly struct ParticipantId(string value)
{
    public string Value { get; } = value;

    public static implicit operator string(ParticipantId participantId) => participantId.Value;
    public static implicit operator ParticipantId(string value) => new(value);
}