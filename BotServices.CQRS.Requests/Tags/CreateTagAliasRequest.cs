using BotServices.CQRS.Responses.Tags;
using Disqord.Bot.Commands.Application;

namespace BotServices.CQRS.Requests.Tags;

public record CreateTagAliasRequest :
    DiscordRequestBase<IDiscordApplicationGuildCommandContext, CreateTagAliasResponse>
{
    public required string TagName { get; init; }
    public required string AliasName { get; init; }
}