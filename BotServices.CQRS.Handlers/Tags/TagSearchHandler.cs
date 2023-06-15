using BotServices.CQRS.Requests.Tags;
using BotServices.CQRS.Responses.Tags;
using BotServices.Services.Core;
using MediatR;

namespace BotServices.CQRS.Handlers.Tags;

public class TagSearchHandler : IRequestHandler<TagSearchRequest, TagSearchResponse>
{
    private readonly ITagService _tagService;

    public TagSearchHandler(ITagService tagService)
    {
        _tagService = tagService;
    }

    public async Task<TagSearchResponse> Handle(TagSearchRequest request, CancellationToken cancellationToken)
    {
        var tags = await _tagService
            .GetTagsAsync(request.Context.GuildId, request.Prompt);

        return new TagSearchResponse
        {
            Context = request.Context,
            FoundTags = tags
        };
    }
}