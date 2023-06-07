namespace BotServices.Entities.Tags;

public static class Extensions
{
    /// <summary>
    /// Safely gets <see cref="TagMessage"/> from this <see cref="Tag"/> regardless of its type.
    /// </summary>
    /// <param name="tag"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public static TagMessage GetTagMessage(this Tag tag) => tag switch
    {
        TagMessage tagMessage => tagMessage,
        TagAlias tagAlias => tagAlias.ReferencedTag,
        _ => throw new InvalidOperationException()
    };
}