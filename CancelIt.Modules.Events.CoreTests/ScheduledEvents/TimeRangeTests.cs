using CancelIt.Modules.Events.Core.ScheduledEvents;
using NUnit.Framework;

namespace CancelIt.Modules.Events.CoreTests.ScheduledEvents;

public class TimeRangeTests
{
    [Test]
    public void ValidTimeRange()
    {
        var start = new DateTimeOffset(2024, 06, 06, 1, 0, 0, TimeSpan.Zero);
        var end = new DateTimeOffset(2024, 06, 06, 2, 0, 0, TimeSpan.Zero);
        
        var timeRange = new TimeRange(start, end);
        Assert.Multiple(() =>
        {
            Assert.That(timeRange.Start, Is.EqualTo(start));
            Assert.That(timeRange.End, Is.EqualTo(end));
            Assert.That(timeRange.Duration, Is.EqualTo(TimeSpan.FromHours(1)));
        });
    }

    [Test]
    public void ZeroDuration()
    {
        var start = new DateTimeOffset(2024, 06, 06, 1, 0, 0, TimeSpan.Zero);
        var end = new DateTimeOffset(2024, 06, 06, 1, 0, 0, TimeSpan.Zero);
        
        Assert.That(() => new TimeRange(start, end), Throws.ArgumentException);
    }
    
    [Test]
    public void NegativeDuration()
    {
        var start = new DateTimeOffset(2024, 06, 06, 2, 0, 0, TimeSpan.Zero);
        var end = new DateTimeOffset(2024, 06, 06, 1, 0, 0, TimeSpan.Zero);
        
        Assert.That(() => new TimeRange(start, end), Throws.ArgumentException);
    }
}