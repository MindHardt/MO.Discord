namespace Domain.Options;

public record CacheOptions
{
    public TimeSpan TagExpiration { get; init; } = TimeSpan.FromMinutes(15);
    public TimeSpan GuildExpiration { get; init; } = TimeSpan.FromMinutes(5);
}