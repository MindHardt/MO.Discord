using Data.Entities.Tags;
using Disqord;

namespace Data.Entities.Users;

public record UserData
{
    public required Snowflake UserId { get; init; }
    public ICollection<Tag> Tags { get; init; } = new List<Tag>();
    
    public UserAccessLevel AccessLevel { get; init; } = UserAccessLevel.Default;
    public int? CustomTagLimit { get; init; }
}