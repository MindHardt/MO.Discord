using Domain.Commands.Requests.Tags;
using Domain.Commands.Responses.Tags;
using Domain.Exceptions;
using Domain.Services.Abstractions.Tags;
using MediatR;

namespace Domain.Commands.Handlers;

public class GetTagRequestHandler : IRequestHandler<GetTagRequest, GetTagResponse>
{
    private readonly ITagService _tagService;

    public GetTagRequestHandler(ITagService tagService)
    {
        _tagService = tagService;
    }

    public async Task<GetTagResponse> Handle(GetTagRequest request, CancellationToken cancellationToken)
    {
        var tag = await _tagService.GetTagAsync(request.TagName, request.GuildId);
        NotFoundException.ThrowIfNull(tag);

        return new GetTagResponse
        {
            FoundTag = tag
        };
    }
}