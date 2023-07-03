using Disqord;
using Domain.Dispatcher.Core;
using Domain.Dispatcher.Responses.Tags;

namespace Domain.Dispatcher.Default.Formatters;

public class GetTagResponseFormatter : IFormatter<GetTagResponse, LocalInteractionMessageResponse>
{
    public LocalInteractionMessageResponse Format(GetTagResponse source)
        => new LocalInteractionMessageResponse().WithContent(source.FoundTag.Content);
}