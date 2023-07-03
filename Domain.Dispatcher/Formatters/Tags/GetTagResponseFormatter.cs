using Disqord;
using Domain.Dispatcher.Core;
using Domain.Dispatcher.Responses.Tags;

namespace Domain.Dispatcher.Formatters.Tags;

public class GetTagResponseFormatterInteraction : IFormatter<GetTagResponse, LocalInteractionMessageResponse>
{
    public LocalInteractionMessageResponse Format(GetTagResponse source)
        => new LocalInteractionMessageResponse().WithContent(source.FoundTag.Content);
}

public class GetTagResponseFormatter : IFormatter<GetTagResponse, LocalMessage>
{
    public LocalMessage Format(GetTagResponse source)
        => new LocalMessage().WithContent(source.FoundTag.Content);
}