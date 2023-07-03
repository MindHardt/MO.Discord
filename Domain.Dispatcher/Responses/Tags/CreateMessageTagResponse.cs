using Data.Entities.Tags;

namespace Domain.Dispatcher.Responses.Tags;

public record CreateMessageTagResponse
{
    public required Tag CreatedTag { get; init; }
}