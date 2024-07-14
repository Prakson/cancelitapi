namespace CancelIt.Modules.Events.Core.ScheduledEvents;

public readonly struct TimeRange
{
    public TimeRange(DateTimeOffset start, DateTimeOffset end)
    {
        var duration = end - start;
        if (duration == TimeSpan.Zero)
        {
            throw new ArgumentException("Duration must be greater than zero");
        }
        
        if (duration < TimeSpan.Zero)
        {
            throw new ArgumentException("Start date must be before end date");
        }
        
        Start = start;
        End = end;
    }
    
    
    public DateTimeOffset Start { get; }
    public DateTimeOffset End { get; }
    
    public TimeSpan Duration => End - Start;
}