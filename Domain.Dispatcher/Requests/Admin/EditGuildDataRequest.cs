using Disqord;
using Domain.Dispatcher.Responses.Admin;
using MediatR;

namespace Domain.Dispatcher.Requests.Admin;

public class EditGuildDataRequest : IRequest<EditGuildDataResponse>
{
    public required Snowflake GuildId { get; init; }
    public bool? InlineTagsEnabled { get; init; }
    public string? InlineTagPrefix { get; init; }
    
}