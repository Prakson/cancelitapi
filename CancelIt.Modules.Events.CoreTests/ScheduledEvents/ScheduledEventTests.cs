using CancelIt.Modules.Events.Core.ScheduledEvents;
using CancelIt.Modules.Events.Core.ScheduledEvents.Events;
using NUnit.Framework;

namespace CancelIt.Modules.Events.CoreTests.ScheduledEvents;

public class ScheduledEventTests
{
    private readonly string _hostIdentity = Guid.NewGuid().ToString();
    private readonly string _name = "My test event";
    
    private readonly TimeRange _timeRange = new(
        new(2024, 06, 06, 06, 00, 00, TimeSpan.Zero),
        new(2024, 06, 06, 06, 30, 00, TimeSpan.Zero));
    
    
    [Test]
    public void NewEvent()
    {
        var scheduledEvent = new ScheduledEvent(_hostIdentity, _name, _timeRange);
        
        Assert.That(scheduledEvent.HostIdentity, Is.EqualTo(_hostIdentity));
        Assert.That(scheduledEvent.EventName, Is.EqualTo(_name));
        Assert.That(scheduledEvent.TimeRange, Is.EqualTo(_timeRange));
        Assert.That(scheduledEvent.Participants, Is.Empty);
        
        Assert.That(scheduledEvent.DomainEvents, Has.Count.EqualTo(1));
        var e = (EventScheduled) scheduledEvent.DomainEvents.First();
        Assert.That(scheduledEvent, Is.SameAs(e.ScheduledEvent));
    }

    [Test]
    public void AddParticipant()
    {
        var scheduledEvent = new ScheduledEvent(_hostIdentity, _name, _timeRange);
        var participant1 = Guid.NewGuid().ToString();
        var participant2 = Guid.NewGuid().ToString();
        var participant3 = Guid.NewGuid().ToString();

        scheduledEvent.Join(participant1);
        scheduledEvent.Join(participant2);
        scheduledEvent.Join(participant3);

        Assert.That(scheduledEvent.Participants, Has.Count.EqualTo(3));
        Assert.That(scheduledEvent.Participants, Has.Member(participant1));
        Assert.That(scheduledEvent.Participants, Has.Member(participant2));
        Assert.That(scheduledEvent.Participants, Has.Member(participant3));
        
        var events = scheduledEvent.DomainEvents.Where(x => x.GetType() == typeof(ParticipantJoined)).ToList();
        Assert.That(events, Has.Count.EqualTo(3));
    }

    [Test]
    public void SingleUserEventRequestCancellation()
    {
        var scheduledEvent = new ScheduledEvent(_hostIdentity, _name, _timeRange);

        scheduledEvent.RequestCancellation(_hostIdentity);
        
        
    }
}