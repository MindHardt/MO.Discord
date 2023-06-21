using Disqord;
using Domain.Commands.Responses.Tags;

namespace Domain.Commands.Formatters.Tags;

public class GetTagResponseFormatter : IFormatter<GetTagResponse, LocalInteractionMessageResponse>
{
    public LocalInteractionMessageResponse Format(GetTagResponse source)
        => new LocalInteractionMessageResponse().WithContent(source.FoundTag.Content);
}