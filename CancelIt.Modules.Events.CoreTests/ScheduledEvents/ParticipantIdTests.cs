using CancelIt.Modules.Events.Core.ScheduledEvents;
using NUnit.Framework;

namespace CancelIt.Modules.Events.CoreTests.ScheduledEvents;

public class ParticipantIdTests
{
    [Test]
    public void Valid()
    {
        var participantId = new ParticipantId("123");
        Assert.That(participantId.Value, Is.EqualTo("123"));
        Assert.That((string) participantId, Is.EqualTo("123"));
    }

    [Test]
    public void Cast()
    {
        var participantId = (ParticipantId) "123";
        Assert.That(participantId.Value, Is.EqualTo("123"));
        Assert.That((string) participantId, Is.EqualTo("123"));
    }
}