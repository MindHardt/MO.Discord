using System.ComponentModel.DataAnnotations;
using BotServices.CQRS.Requests.Tags;
using BotServices.CQRS.Responses.Tags;
using BotServices.Entities.Tags;
using BotServices.Exceptions;
using BotServices.Factories.Core;
using BotServices.Services.Core;
using Disqord;
using MediatR;

namespace BotServices.CQRS.Handlers.Tags;

public class CreateTagAliasHandler :
    IRequestHandler<CreateTagAliasRequest, CreateTagAliasResponse>
{
    private readonly ITagService _tagService;
    private readonly ITagFactory _tagFactory;
    private readonly ITagNameService _tagNameService;

    public CreateTagAliasHandler(
        ITagService tagService, 
        ITagFactory tagFactory, 
        ITagNameService tagNameService)
    {
        _tagService = tagService;
        _tagFactory = tagFactory;
        _tagNameService = tagNameService;
    }

    public async Task<CreateTagAliasResponse> Handle(CreateTagAliasRequest request, CancellationToken cancellationToken)
    {
        if (_tagNameService.TagNameValid(request.AliasName) is false)
            throw new ValidationException("Имя тега недопустимо!");
            
        Snowflake guildId = request.Context.GuildId;

        Tag? tag = await _tagService.GetTagAsync(request.TagName, guildId);

        NotFoundException.ThrowIfNull(tag);

        TagMessage tagMessage = tag.GetTagMessage();
        Snowflake ownerId = request.Context.AuthorId;

        TagAlias alias = _tagFactory.CreateTagAlias(tagMessage, request.AliasName, ownerId, guildId);

        await _tagService.SaveTagAsync(alias, ownerId, await request.Context.Bot.IsOwnerAsync(ownerId));

        return new CreateTagAliasResponse
        {
            Context = request.Context,
            Alias = alias
        };
    }
}