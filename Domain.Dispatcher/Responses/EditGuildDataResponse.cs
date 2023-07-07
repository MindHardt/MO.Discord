using Data.Entities;

namespace Domain.Dispatcher.Responses;

public record EditGuildDataResponse
{
    public required GuildData GuildData { get; init; }
}