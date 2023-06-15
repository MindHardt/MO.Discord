using BotServices.CQRS.Requests.Tags;
using BotServices.CQRS.Responses.Tags;
using BotServices.Factories.Core;
using BotServices.Services.Core;
using Disqord;
using MediatR;

namespace BotServices.CQRS.Handlers.Tags;

public class CreateTagHandler : IRequestHandler<CreateTagRequest, CreateTagResponse>
{
    private readonly ITagFactory _tagFactory;
    private readonly ITagService _tagService;

    public CreateTagHandler(ITagFactory tagFactory, ITagService tagService)
    {
        _tagFactory = tagFactory;
        _tagService = tagService;
    }

    public async Task<CreateTagResponse> Handle(CreateTagRequest request, CancellationToken cancellationToken)
    {
        Snowflake ownerId = request.Context.AuthorId;
        Snowflake guildId = request.Context.GuildId;
        var tag = _tagFactory.CreateTagMessage(request.Name, request.Text, ownerId, guildId);

        await _tagService.SaveTagAsync(tag, ownerId, await request.Context.Bot.IsOwnerAsync(ownerId));

        return new CreateTagResponse
        {
            Context = request.Context,
            CreatedTag = tag
        };
    }
}