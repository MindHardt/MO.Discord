using Disqord;
using Domain.Dispatcher.Responses;
using MediatR;
using Qommon;

namespace Domain.Dispatcher.Requests;

public record EditGuildDataRequest : IRequest<EditGuildDataResponse>
{
    public required Snowflake GuildId { get; init; }
    public Optional<string> GuildName { get; init; }
    public Optional<bool> InlineTagsEnabled { get; init; }
    public Optional<string> InlineTagPrefix { get; init; }
    public Optional<bool> AdultAllowed { get; init; }
}