using Domain.Models;

namespace Domain.Dispatcher.Responses.Images;

public record GetColorResponse
{
    public required NamedStream Image { get; init; }
}