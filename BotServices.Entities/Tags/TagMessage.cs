using System.ComponentModel.DataAnnotations;

namespace BotServices.Entities.Tags;

public record TagMessage : Tag
{
    [MaxLength(Constants.MaxContentLength)]
    public required string Text { get; set; }

    public ICollection<TagAlias> Aliases { get; set; } = new List<TagAlias>();
    
    public override string Content => Text;
}