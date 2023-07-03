using Disqord;

namespace Domain.Options;

public record DiscordOptions
{
    public string? Token { get; init; }
    public ulong[]? OwnerIds { get; init; }

    public IEnumerable<Snowflake>? OwnerSnowflakes => OwnerIds?.Select(id => (Snowflake)id);
}