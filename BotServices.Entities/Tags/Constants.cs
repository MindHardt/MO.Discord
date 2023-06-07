namespace BotServices.Entities.Tags;

/// <summary>
/// Contains various constants connected to tags.
/// </summary>
public static class Constants
{
    /// <summary>
    /// The maximum length of <see cref="TagMessage"/>.<see cref="TagMessage.Text"/>.
    /// </summary>
    public const int MaxContentLength = 2048;
    /// <summary>
    /// The maximum length of <see cref="Tag"/>.<see cref="Tag.Name"/>.
    /// </summary>
    public const int MaxNameLength = 64;
}