using BotServices.Entities.Tags;
using Disqord.Bot.Commands.Application;

namespace BotServices.CQRS.Responses.Tags;

public record DeleteTagResponse : 
    DiscordResponseBase<IDiscordApplicationGuildCommandContext>
{
    public required Tag DeletedTag { get; init; }
}