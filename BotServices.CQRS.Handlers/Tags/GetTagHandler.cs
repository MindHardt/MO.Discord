using BotServices.CQRS.Requests.Tags;
using BotServices.CQRS.Responses.Tags;
using BotServices.Entities.Tags;
using BotServices.Exceptions;
using BotServices.Services.Core;
using Disqord;
using MediatR;

namespace BotServices.CQRS.Handlers.Tags;

public class GetTagHandler : IRequestHandler<GetTagRequest, GetTagResponse>
{
    private readonly ITagService _tagService;

    public GetTagHandler(ITagService tagService)
    {
        _tagService = tagService;
    }

    public async Task<GetTagResponse> Handle(GetTagRequest request, CancellationToken cancellationToken)
    {
        Snowflake guildId = request.Context.GuildId;
        Tag? tag = await _tagService.GetTagAsync(request.TagName, guildId);

        NotFoundException.ThrowIfNull(tag);

        return new GetTagResponse
        {
            Context = request.Context,
            Tag = tag
        };
    }
}