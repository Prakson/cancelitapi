namespace CancelIt.Modules.Events.Core.ScheduledEvents.Exceptions;

public class InvalidTimeRange(string message) : Exception(message)
{
    public static InvalidTimeRange NegativeDuration() => new("Start date must be before end date");
    public static InvalidTimeRange ZeroDuration() => new("Duration must be greater than zero");
}