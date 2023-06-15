using BotServices.CQRS.Responses.Tags;
using Disqord.Bot.Commands.Application;

namespace BotServices.CQRS.Requests.Tags;

public record TagSearchRequest : 
    DiscordRequestBase<IDiscordApplicationGuildCommandContext, TagSearchResponse> 
{
    public required string Prompt { get; init; }
}