using Disqord;
using Domain.Dispatcher.Core;
using Domain.Models;

namespace Domain.Dispatcher.Mappers;

public class NamedStreamMessageMapper : IMessageMapper<NamedStream>
{
    public TMessage MapAs<TMessage>(NamedStream source) where TMessage : LocalMessageBase, new()
    {
        return new TMessage()
            .AddAttachment(new LocalAttachment(source.Content, source.Name));
    }
}