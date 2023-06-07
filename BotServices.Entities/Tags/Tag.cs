using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Disqord;

namespace BotServices.Entities.Tags;

public abstract record Tag
{
    /// <summary>
    /// The database Id of the tag.
    /// </summary>
    public long Id { get; set; }
    /// <summary>
    /// The name of the tag. It must be unique per origin.
    /// </summary>
    [MaxLength(Constants.MaxNameLength)]
    public required string Name { get; set; }
    /// <summary>
    /// The owners discord Id.
    /// </summary>
    public required Snowflake OwnerId { get; set; }
    public Snowflake? GuildId { get; set; }
    public string? Discriminator { get; set; }
    public abstract string Content { get; }

    /// <summary>
    /// Defines whether this <see cref="Tag"/> is public (accessible from any guild).
    /// </summary>
    [NotMapped]
    public bool IsPublic => GuildId is null;
}