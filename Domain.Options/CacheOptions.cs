namespace Domain.Options;

public record CacheOptions
{
    public TimeSpan TagExpiration { get; init; } = TimeSpan.FromMinutes(15);
}