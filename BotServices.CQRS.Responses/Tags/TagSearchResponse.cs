using BotServices.Entities.Tags;
using Disqord.Bot.Commands.Application;

namespace BotServices.CQRS.Responses.Tags;

public record TagSearchResponse : 
    DiscordResponseBase<IDiscordApplicationGuildCommandContext>
{
    public required IReadOnlyCollection<Tag> FoundTags { get; init; }
}