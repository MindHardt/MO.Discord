using Data.Entities.Tags;

namespace Domain.Commands.Responses.Tags;

public record CreateMessageTagResponse
{
    public required Tag CreatedTag { get; init; }
}