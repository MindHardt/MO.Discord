using Data.Entities.Tags;

namespace Domain.Dispatcher.Responses.Tags;

public record GetTagResponse
{
    public required Tag FoundTag { get; init; }
}