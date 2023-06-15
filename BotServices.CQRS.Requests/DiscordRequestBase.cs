using MediatR;
using Qmmands;

namespace BotServices.CQRS.Requests;

/// <summary>
/// A base class for requests that are related to guild slash commands.
/// </summary>
public abstract record DiscordRequestBase<TContext, TResponse>
    : IRequest<TResponse>
    where TContext: ICommandContext
{
    public required TContext Context { get; init; }
}