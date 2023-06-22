using Data.Entities.Tags;
using Domain.Commands.Requests.Tags;
using Domain.Commands.Responses.Tags;
using Domain.Factories.Abstractions;
using Domain.Services.Abstractions;
using Domain.Services.Abstractions.Tags;
using MediatR;

namespace Domain.Commands.Handlers.Tags;

public class CreateMessageTagRequestHandler : IRequestHandler<CreateMessageTagRequest, CreateMessageTagResponse>
{
    private readonly ITagFactory _tagFactory;
    private readonly ITagService _tagService;
    private readonly IUserService _userService;

    public CreateMessageTagRequestHandler(
        ITagFactory tagFactory, 
        ITagService tagService, 
        IUserService userService)
    {
        _tagFactory = tagFactory;
        _tagService = tagService;
        _userService = userService;
    }

    public async Task<CreateMessageTagResponse> Handle(CreateMessageTagRequest request, CancellationToken cancellationToken)
    {
        var user = await _userService.GetOrCreateAsync(request.AuthorId);
        
        Tag tag = _tagFactory.CreateMessageTag(
            request.TagText,
            request.TagName,
            user,
            request.GuildId);

        tag = await _tagService.SaveTagAsync(tag, request.AuthorId);

        return new CreateMessageTagResponse
        {
            CreatedTag = tag
        };
    }
}