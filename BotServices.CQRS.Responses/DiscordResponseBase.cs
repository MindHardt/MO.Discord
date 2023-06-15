using Qmmands;

namespace BotServices.CQRS.Responses;

/// <summary>
/// A base class for requests that are related to guild slash commands.
/// </summary>
public abstract record DiscordResponseBase<TContext>
    where TContext : ICommandContext
{
    public required TContext Context { get; init; }
}