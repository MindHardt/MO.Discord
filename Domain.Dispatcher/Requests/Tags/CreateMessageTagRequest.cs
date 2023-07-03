using Disqord;
using Domain.Dispatcher.Responses.Tags;
using MediatR;

namespace Domain.Dispatcher.Requests.Tags;

public record CreateMessageTagRequest : IRequest<CreateMessageTagResponse>
{
    public string TagName { get; init; } = Guid.NewGuid().ToString("N");
    public required string TagText { get; init; }
    public required Snowflake? GuildId { get; init; }
    public required Snowflake AuthorId { get; init; }
}