using System.Threading.Channels;
using CancelIt.Shared.Events;

namespace CancelIt.Shared.Messaging;

internal sealed class EventChannel
{
    private readonly Channel<Event> _messages = Channel.CreateUnbounded<Event>();

    public ChannelReader<Event> Reader => _messages.Reader;
    public ChannelWriter<Event> Writer => _messages.Writer;
}