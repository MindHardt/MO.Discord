using BotServices.CQRS.Responses.Tags;
using BotServices.Factories.Core;
using Qmmands;

namespace BotServices.CQRS.ResponseFormatters.Default.Tags;

public class CreateTagAliasDiscordResponseFormatter :
    DefaultApplicationGuildResponseFormatterBase<CreateTagAliasResponse>
{
    private readonly IDiscordResponseFactory _discordResponseFactory;

    public CreateTagAliasDiscordResponseFormatter(IDiscordResponseFactory discordResponseFactory)
    {
        _discordResponseFactory = discordResponseFactory;
    }

    public override IResult FormatResponse(CreateTagAliasResponse response)
    {
        var message = $"Создал псевдоним`{response.Alias.Name}` ➡️ `{response.Alias.ReferencedTag.Name}`";

        return MessageResponse(response, _discordResponseFactory.GetSuccessfulResponse(message));
    }
}