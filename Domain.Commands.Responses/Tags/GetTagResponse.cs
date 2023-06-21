using Data.Entities.Tags;

namespace Domain.Commands.Responses.Tags;

public record GetTagResponse
{
    public required Tag FoundTag { get; init; }
}