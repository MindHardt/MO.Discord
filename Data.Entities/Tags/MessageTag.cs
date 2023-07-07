using System.ComponentModel.DataAnnotations;

namespace Data.Entities.Tags;

/// <summary>
/// A tag made from discord message.
/// </summary>
public record MessageTag : Tag
{
    public const int MaxTextLength = 2000;
    public override string Content => Text;
    
    [MaxLength(MaxTextLength)]
    public required string Text { get; set; }
}