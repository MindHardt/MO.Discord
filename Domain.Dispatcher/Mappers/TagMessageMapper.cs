using Data.Entities.Tags;
using Disqord;
using Domain.Dispatcher.Core;

namespace Domain.Dispatcher.Mappers;

public class TagMessageMapper : IMessageMapper<Tag>
{
    public TMessage MapAs<TMessage>(Tag source) where TMessage : LocalMessageBase, new()
    {
        return new TMessage().WithContent(source.Content);
    }
}