using BotServices.CQRS.Responses.Tags;
using BotServices.Entities.Tags;
using BotServices.Factories.Core;
using BotServices.Services.Core;
using Disqord;
using Qmmands;

namespace BotServices.CQRS.ResponseFormatters.Default.Tags;

public class TagSearchDiscordResponseFormatter : 
    DefaultApplicationGuildResponseFormatterBase<TagSearchResponse>
{
    private readonly ITagService _tagService;
    private readonly IDiscordResponseFactory _discordResponseFactory;

    public TagSearchDiscordResponseFormatter(
        ITagService tagService, 
        IDiscordResponseFactory discordResponseFactory)
    {
        _tagService = tagService;
        _discordResponseFactory = discordResponseFactory;
    }

    public override IResult FormatResponse(TagSearchResponse response)
    {
        var fields = response.FoundTags
            .Select((tag, i) => CreateField(tag).WithName($"{i + 1}"))
            .Take(25);

        var message = _discordResponseFactory.GetSuccessfulResponse();
        message.Embeds.Value[0].Fields = fields.ToList();
        
        return MessageResponse(response, message);
    }

    private LocalEmbedField CreateField(Tag tag)
        => new LocalEmbedField()
            .WithIsInline()
            .WithValue($"`{_tagService.CreateOverview(tag)}`");
}