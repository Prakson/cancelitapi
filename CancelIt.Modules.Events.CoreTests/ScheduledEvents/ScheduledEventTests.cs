using CancelIt.Modules.Events.Core.Aggregates;
using CancelIt.Modules.Events.Core.Events;
using NUnit.Framework;

namespace CancelIt.Modules.Events.CoreTests.ScheduledEvents;

public class ScheduledEventTests
{
    private readonly string _hostIdentity = Guid.NewGuid().ToString();
    private readonly string _name = "My test event";
    private readonly DateTimeOffset _startDate = new(2024, 06, 06, 06, 00, 00, TimeSpan.Zero);
    private readonly DateTimeOffset _endDate = new(2024, 06, 06, 06, 00, 00, TimeSpan.Zero);    
    
    
    [Test]
    public void NewEvent()
    {
        var scheduledEvent = new ScheduledEvent(_hostIdentity, _name, _startDate, _endDate);
        
        Assert.That(scheduledEvent.HostIdentity, Is.EqualTo(_hostIdentity));
        Assert.That(scheduledEvent.EventName, Is.EqualTo(_name));
        Assert.That(scheduledEvent.StartTime, Is.EqualTo(_startDate));
        Assert.That(scheduledEvent.EndTime, Is.EqualTo(_endDate));
        Assert.That(scheduledEvent.Participants, Is.Empty);
        
        Assert.That(scheduledEvent.DomainEvents, Has.Count.EqualTo(1));
        var e = (EventScheduled) scheduledEvent.DomainEvents.First();
        Assert.That(scheduledEvent, Is.SameAs(e.ScheduledEvent));
    }

    [Test]
    public void AddParticipant()
    {
        var scheduledEvent = new ScheduledEvent(_hostIdentity, _name, _startDate, _endDate);
        var participant1 = Guid.NewGuid().ToString();
        var participant2 = Guid.NewGuid().ToString();
        var participant3 = Guid.NewGuid().ToString();

        scheduledEvent.Invite(participant1);
        scheduledEvent.Invite(participant2);
        scheduledEvent.Invite(participant3);

        Assert.That(scheduledEvent.Participants, Has.Count.EqualTo(3));
        Assert.That(scheduledEvent.Participants, Has.Member(participant1));
        Assert.That(scheduledEvent.Participants, Has.Member(participant2));
        Assert.That(scheduledEvent.Participants, Has.Member(participant3));
        
        var events = scheduledEvent.DomainEvents.Where(x => x.GetType() == typeof(ParticipantInvited)).ToList();
        Assert.That(events, Has.Count.EqualTo(3));

    }
}