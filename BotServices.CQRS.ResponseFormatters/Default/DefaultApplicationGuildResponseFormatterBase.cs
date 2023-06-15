using BotServices.CQRS.ResponseFormatters.Core;
using BotServices.CQRS.Responses;
using Disqord;
using Disqord.Bot.Commands.Application;
using Disqord.Bot.Commands.Interaction;
using Qmmands;

namespace BotServices.CQRS.ResponseFormatters.Default;

public abstract class DefaultApplicationGuildResponseFormatterBase<TResponse>
    : IDiscordResponseFormatter<TResponse>
    where TResponse: DiscordResponseBase<IDiscordApplicationGuildCommandContext>
{
    public abstract IResult FormatResponse(TResponse response);

    protected IResult MessageResponse(TResponse response, LocalInteractionMessageResponse message)
        => new DiscordInteractionResponseCommandResult(response.Context, message);
}