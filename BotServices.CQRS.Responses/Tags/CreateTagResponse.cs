using BotServices.Entities.Tags;
using Disqord.Bot.Commands.Application;

namespace BotServices.CQRS.Responses.Tags;

public record CreateTagResponse : 
    DiscordResponseBase<IDiscordApplicationGuildCommandContext>
{
    public required Tag CreatedTag { get; init; }
}