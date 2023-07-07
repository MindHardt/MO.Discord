using Data.Entities.Tags;
using Disqord;

namespace Data.Entities.Users;

public record UserData
{
    public required Snowflake UserId { get; set; }
    public string UserName { get; set; } = string.Empty;
    public ICollection<Tag> Tags { get; set; } = new List<Tag>();
    
    public AccessLevel AccessLevel { get; set; } = AccessLevel.Default;
    public int? CustomTagLimit { get; set; }
}