using CancelIt.Modules.Events.Core.ScheduledEvents;
using CancelIt.Modules.Events.Core.ScheduledEvents.Events;
using CancelIt.Modules.Events.Core.ScheduledEvents.Exceptions;
using NUnit.Framework;

namespace CancelIt.Modules.Events.CoreTests.ScheduledEvents;

public class ScheduledEventTests
{
    private readonly string _name = "My test event";
    
    private readonly DateTimeOffset _currentTime = new(2024, 06, 06, 06, 30, 00, TimeSpan.Zero);
    private readonly TimeRange _timeRange = new(
        new(2024, 06, 06, 06, 00, 00, TimeSpan.Zero),
        new(2024, 06, 06, 06, 30, 00, TimeSpan.Zero));
    
    
    [Test]
    public void NewEvent()
    {
        var scheduledEvent = new ScheduledEvent(_name, _timeRange);
        Assert.Multiple(() =>
        {
            Assert.That(scheduledEvent.EventName, Is.EqualTo(_name));
            Assert.That(scheduledEvent.TimeRange, Is.EqualTo(_timeRange));
            Assert.That(scheduledEvent.Participants, Is.Empty);
            Assert.That(scheduledEvent.Status, Is.EqualTo(ScheduledEventStatus.Scheduled));

            Assert.That(scheduledEvent.DomainEvents, Has.Count.EqualTo(1));
            var @event = (EventScheduled) scheduledEvent.DomainEvents.First();
            Assert.That(scheduledEvent, Is.SameAs(@event.ScheduledEvent));
        });
    }

    [Test]
    public void CannotJoinTwice()
    {
        var scheduledEvent = new ScheduledEvent(_name, _timeRange);
        var participant1 = (ParticipantId) Guid.NewGuid().ToString();
        
        scheduledEvent.Join(participant1, _currentTime);
        
        Assert.That(() => scheduledEvent.Join(participant1, _currentTime), Throws.TypeOf<AlreadyParticipates>());
    } 
    
    [Test]
    public void CannotJoinCancelledEvent()
    {
        var scheduledEvent = new ScheduledEvent(_name, _timeRange);
        var participant1 = (ParticipantId) Guid.NewGuid().ToString();
        var participant2 = (ParticipantId) Guid.NewGuid().ToString();
        
        scheduledEvent.Join(participant1, _currentTime);
        scheduledEvent.RequestCancellation(participant1, _currentTime);
        
        Assert.That(() => scheduledEvent.Join(participant2, _currentTime), Throws.TypeOf<EventAlreadyCancelled>());
    }
    
    [Test]
    public void Join()
    {
        var scheduledEvent = new ScheduledEvent(_name, _timeRange);
        var participant1 = (ParticipantId) Guid.NewGuid().ToString();
        var participant2 = (ParticipantId) Guid.NewGuid().ToString();

        scheduledEvent.Join(participant1, _currentTime);
        scheduledEvent.Join(participant2, _currentTime);

        Assert.That(scheduledEvent.Participants, Has.Count.EqualTo(2));
        Assert.That(scheduledEvent.Participants, Has.Exactly(1).Matches(new Predicate<Participation>(x => x.ParticipantId == participant1)));
        Assert.That(scheduledEvent.Participants, Has.Exactly(1).Matches(new Predicate<Participation>(x => x.ParticipantId == participant2)));
        
        var events = scheduledEvent.DomainEvents.Where(x => x.GetType() == typeof(ParticipantJoined)).ToList();
        var @event1 = (ParticipantJoined) events[0];
        var @event2 = (ParticipantJoined) events[1];
        Assert.Multiple(() =>
        {
            Assert.That(event1.ParticipantIdentity, Is.EqualTo(participant1));
            Assert.That(event1.Event, Is.SameAs(scheduledEvent));
            Assert.That(event2.ParticipantIdentity, Is.EqualTo(participant2));
            Assert.That(event2.Event, Is.SameAs(scheduledEvent));
        });
    }

    [Test]
    public void CannotRequestCancellationForUserThatHasNotJoined()
    {
        var scheduledEvent = new ScheduledEvent(_name, _timeRange);
        var participant1 = (ParticipantId) Guid.NewGuid().ToString();
        
        Assert.That(() => scheduledEvent.RequestCancellation(participant1, _currentTime), Throws.TypeOf<DoesNotParticipate>());
    }
    
    [Test]
    public void CannotRequestCancellationTwice()
    {
        var scheduledEvent = new ScheduledEvent(_name, _timeRange);
        var participant1 = (ParticipantId) Guid.NewGuid().ToString();
        
        scheduledEvent.Join(participant1, _currentTime);
        scheduledEvent.RequestCancellation(participant1, _currentTime);
        
        Assert.That(() => scheduledEvent.RequestCancellation(participant1, _currentTime), Throws.TypeOf<ParticipantAlreadyCancelled>());
    }
    
    [Test]
    public void SingleUserEventRequestCancellation()
    {
        var scheduledEvent = new ScheduledEvent(_name, _timeRange);

        var participant1 = (ParticipantId) Guid.NewGuid().ToString();
        
        scheduledEvent.Join(participant1, _currentTime);
        scheduledEvent.RequestCancellation(participant1, _currentTime);
        
        Assert.That(scheduledEvent.Status, Is.EqualTo(ScheduledEventStatus.Cancelled));
        
        var events = scheduledEvent.DomainEvents.Where(x => x.GetType() == typeof(EventCancelled)).ToList();
        
        Assert.That(events, Has.Count.EqualTo(1));
        var @event = (EventCancelled) events.First();
        Assert.Multiple(() =>
        {
            Assert.That(@event.Event, Is.SameAs(scheduledEvent));
            Assert.That(@event.CausedBy, Is.EqualTo(participant1));
        });
    }
    
    [Test]
    public void MultipleUserEventRequestCancellation()
    {
        var scheduledEvent = new ScheduledEvent(_name, _timeRange);

        var participant1 = (ParticipantId) Guid.NewGuid().ToString();
        var participant2 = (ParticipantId) Guid.NewGuid().ToString();
        
        scheduledEvent.Join(participant1, _currentTime);
        scheduledEvent.Join(participant2, _currentTime);
        
        scheduledEvent.RequestCancellation(participant1, _currentTime);
        scheduledEvent.RequestCancellation(participant2, _currentTime);
        
        Assert.That(scheduledEvent.Status, Is.EqualTo(ScheduledEventStatus.Cancelled));
        
        var events = scheduledEvent.DomainEvents.Where(x => x.GetType() == typeof(EventCancelled)).ToList();
        
        Assert.That(events, Has.Count.EqualTo(1));
        var @event = (EventCancelled) events.First();
        Assert.Multiple(() =>
        {
            Assert.That(@event.Event, Is.SameAs(scheduledEvent));
            Assert.That(@event.CausedBy, Is.EqualTo(participant2));
        });
    }
}