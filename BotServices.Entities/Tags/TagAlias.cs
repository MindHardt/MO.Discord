namespace BotServices.Entities.Tags;

public record TagAlias : Tag
{
    public required long ReferencedTagId { get; set; }
    public required TagMessage ReferencedTag { get; set; }

    public override string Content => ReferencedTag.Content;
}