using BotServices.Entities.Tags;
using Disqord.Bot.Commands.Application;

namespace BotServices.CQRS.Responses.Tags;

public record CreateTagAliasResponse :
    DiscordResponseBase<IDiscordApplicationGuildCommandContext>
{
    public required TagAlias Alias { get; init; }
}