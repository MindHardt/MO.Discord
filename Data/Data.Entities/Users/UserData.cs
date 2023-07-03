using Data.Entities.Tags;
using Disqord;

namespace Data.Entities.Users;

public record UserData
{
    public required Snowflake UserId { get; set; }
    public ICollection<Tag> Tags { get; set; } = new List<Tag>();
    
    public UserAccessLevel AccessLevel { get; set; } = UserAccessLevel.Default;
    public int? CustomTagLimit { get; set; }
}