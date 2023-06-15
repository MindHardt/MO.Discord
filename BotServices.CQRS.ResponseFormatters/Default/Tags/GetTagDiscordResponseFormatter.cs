using BotServices.CQRS.Responses.Tags;
using BotServices.Services.Core;
using Disqord;
using Qmmands;

namespace BotServices.CQRS.ResponseFormatters.Default.Tags;

public class GetTagDiscordResponseFormatter : 
    DefaultApplicationGuildResponseFormatterBase<GetTagResponse>
{
    private readonly ITagService _tagService;

    public GetTagDiscordResponseFormatter(ITagService tagService)
    {
        _tagService = tagService;
    }


    public override IResult FormatResponse(GetTagResponse response)
    {
        var message = _tagService.CreateMessage<LocalInteractionMessageResponse>(response.Tag);
        
        return MessageResponse(response, message);
    }
}