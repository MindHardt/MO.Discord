namespace Data.Entities.Tags;

/// <summary>
/// An alias for existing tags.
/// </summary>
public record AliasTag : Tag
{
    public long ReferencedTagId { get; set; }
    public required Tag ReferencedTag { get; set; }

    public override string Content => ReferencedTag.Content;
}