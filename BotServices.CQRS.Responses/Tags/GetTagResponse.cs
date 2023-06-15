using BotServices.Entities.Tags;
using Disqord.Bot.Commands.Application;

namespace BotServices.CQRS.Responses.Tags;

public record GetTagResponse :
    DiscordResponseBase<IDiscordApplicationGuildCommandContext>
{
    public required Tag Tag { get; init; }
}