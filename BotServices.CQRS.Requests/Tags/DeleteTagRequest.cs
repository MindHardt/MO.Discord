using BotServices.CQRS.Responses.Tags;
using Disqord.Bot.Commands.Application;

namespace BotServices.CQRS.Requests.Tags;

public record DeleteTagRequest :
    DiscordRequestBase<IDiscordApplicationGuildCommandContext, DeleteTagResponse>
{
    public required string TagName { get; init; }
}