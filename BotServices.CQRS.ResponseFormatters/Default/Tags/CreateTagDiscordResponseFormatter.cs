using BotServices.CQRS.Responses.Tags;
using BotServices.Factories.Core;
using Qmmands;

namespace BotServices.CQRS.ResponseFormatters.Default.Tags;

public class CreateTagDiscordResponseFormatter : 
    DefaultApplicationGuildResponseFormatterBase<CreateTagResponse>
{
    private readonly IDiscordResponseFactory _discordResponseFactory;

    public CreateTagDiscordResponseFormatter(IDiscordResponseFactory discordResponseFactory)
    {
        _discordResponseFactory = discordResponseFactory;
    }

    public override IResult FormatResponse(CreateTagResponse response)
    {
        var message = _discordResponseFactory
            .GetSuccessfulResponse($"Успешно сохранил тег `{response.CreatedTag.Name}`");

        return MessageResponse(response, message);
    }
}