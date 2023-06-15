using BotServices.CQRS.Responses.Tags;
using Disqord.Bot.Commands.Application;

namespace BotServices.CQRS.Requests.Tags;

public record GetTagRequest : 
    DiscordRequestBase<IDiscordApplicationGuildCommandContext, GetTagResponse>
{
    public required string TagName { get; init; }
}