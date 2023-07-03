using Disqord;
using Domain.Dispatcher.Responses.Tags;
using MediatR;

namespace Domain.Dispatcher.Requests.Tags;

public record GetTagRequest : IRequest<GetTagResponse>
{
    public required Snowflake GuildId { get; init; }
    public required string TagName { get; init; }
}