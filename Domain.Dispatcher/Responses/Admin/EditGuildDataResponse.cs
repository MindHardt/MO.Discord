using Data.Entities;

namespace Domain.Dispatcher.Responses.Admin;

public record EditGuildDataResponse
{
    public required GuildData GuildData { get; init; }
}