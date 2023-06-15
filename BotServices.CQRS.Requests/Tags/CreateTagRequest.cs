using BotServices.CQRS.Responses.Tags;
using Disqord.Bot.Commands.Application;

namespace BotServices.CQRS.Requests.Tags;

public record CreateTagRequest : 
    DiscordRequestBase<IDiscordApplicationGuildCommandContext, CreateTagResponse>
{
    public required string Name { get; init; }
    public required string Text { get; init; }
}