using Disqord;
using Domain.Commands.Responses.Tags;
using MediatR;

namespace Domain.Commands.Requests.Tags;

public record GetTagRequest : IRequest<GetTagResponse>
{
    public required Snowflake GuildId { get; init; }
    public required string TagName { get; init; }
}