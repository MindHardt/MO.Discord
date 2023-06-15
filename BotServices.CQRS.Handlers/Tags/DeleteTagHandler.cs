using BotServices.CQRS.Requests.Tags;
using BotServices.CQRS.Responses.Tags;
using BotServices.Services.Core;
using MediatR;

namespace BotServices.CQRS.Handlers.Tags;

public class DeleteTagHandler : 
    IRequestHandler<DeleteTagRequest, DeleteTagResponse>
{
    private readonly ITagService _tagService;

    public DeleteTagHandler(ITagService tagService)
    {
        _tagService = tagService;
    }

    public async Task<DeleteTagResponse> Handle(DeleteTagRequest request, CancellationToken cancellationToken)
    {
        var deletedTag = await _tagService
            .DeleteTagAsync(request.TagName, request.Context.AuthorId);

        return new DeleteTagResponse
        {
            Context = request.Context,
            DeletedTag = deletedTag
        };
    }
}