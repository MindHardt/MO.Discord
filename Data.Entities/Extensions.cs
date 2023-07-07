using Data.Entities.Tags;
using Disqord;

namespace Data.Entities;

public static class Extensions
{
    /// <summary>
    /// Defines whether this tag is considered public and therefore visible in all guilds.
    /// </summary>
    /// <param name="tag"></param>
    /// <returns></returns>
    public static bool IsPublic(this Tag tag) 
        => tag.GuildId is null;
    
    /// <summary>
    /// Defines whether this tag can be seen in guild with <paramref name="guildId"/>.
    /// </summary>
    /// <param name="tag"></param>
    /// <param name="guildId"></param>
    /// <returns></returns>
    public static bool VisibleWithin(this Tag tag, Snowflake guildId) 
        => tag.IsPublic() || tag.GuildId == guildId;
}