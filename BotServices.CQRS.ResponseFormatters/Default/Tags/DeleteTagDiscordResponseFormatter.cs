using BotServices.CQRS.Responses.Tags;
using BotServices.Factories.Core;
using Qmmands;

namespace BotServices.CQRS.ResponseFormatters.Default.Tags;

public class DeleteTagDiscordResponseFormatter :
    DefaultApplicationGuildResponseFormatterBase<DeleteTagResponse>
{
    private readonly IDiscordResponseFactory _discordResponseFactory;

    public DeleteTagDiscordResponseFormatter(IDiscordResponseFactory discordResponseFactory)
    {
        _discordResponseFactory = discordResponseFactory;
    }

    public override IResult FormatResponse(DeleteTagResponse response)
    {
        var message = _discordResponseFactory
            .GetSuccessfulResponse($"Успешно удалил тег `{response.DeletedTag.Name}`");

        return MessageResponse(response, message);
    }
}