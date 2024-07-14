using CancelIt.Modules.Events.Core.ScheduledEvents.Exceptions;

namespace CancelIt.Modules.Events.Core.ScheduledEvents;

public readonly struct TimeRange
{
    public TimeRange(DateTimeOffset start, DateTimeOffset end)
    {
        var duration = end - start;
        if (duration == TimeSpan.Zero)
        {
            throw InvalidTimeRange.ZeroDuration();
        }
        
        if (duration < TimeSpan.Zero)
        {
            throw InvalidTimeRange.NegativeDuration();
        }
        
        Start = start;
        End = end;
    }
    
    
    public DateTimeOffset Start { get; }
    public DateTimeOffset End { get; }
    
    public TimeSpan Duration => End - Start;
}