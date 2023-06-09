using Disqord;

namespace BotServices.Factories.Core;

public interface IDiscordResponseFactory
{
    /// <summary>
    /// Gets default <see cref="LocalInteractionMessageResponse"/> that indicates that operation succeeded.
    /// It can include additional information specified in <paramref name="info"/>.
    /// </summary>
    /// <returns></returns>
    public LocalInteractionMessageResponse GetSuccessfulResponse(string? info = null);

    /// <summary>
    /// Gets default <see cref="LocalInteractionMessageResponse"/> that indicates that operation failed.
    /// It can include additional information specified in <paramref name="info"/>.
    /// </summary>
    /// <returns></returns>
    public LocalInteractionMessageResponse GetFailedResponse(string? info = null);
    
    /// <summary>
    /// Gets a <see cref="LocalInteractionModalResponse"/> that is used to create tags from messages.
    /// </summary>
    /// <returns></returns>
    public LocalInteractionModalResponse GetTagCreationModal(string? customId = null);
}