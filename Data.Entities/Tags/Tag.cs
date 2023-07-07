using Data.Entities.Users;
using Disqord;

namespace Data.Entities.Tags;

/// <summary>
/// A base class for <see cref="Tag"/>s.
/// </summary>
public abstract record Tag
{
    public const int MaxNameLength = 64;
    
    public long Id { get; set; }
    public required string Name { get; set; }
    
    public UserData? Owner { get; set; }
    public required Snowflake OwnerId { get; set; }
    
    public GuildData? Guild { get; set; }
    public required Snowflake? GuildId { get; set; }
    
    public ICollection<AliasTag> Aliases { get; set; } = new List<AliasTag>();
    public abstract string Content { get; }
}