using Disqord;

namespace BotServices.Services.Core;

public interface IResponseService : IBotService
{
    /// <summary>
    /// Gets default <see cref="LocalInteractionMessageResponse"/> that indicates that operation succeeded.
    /// </summary>
    /// <returns></returns>
    public LocalInteractionMessageResponse GetSuccessfulResponse();

    /// <summary>
    /// Gets default <see cref="LocalInteractionMessageResponse"/> that indicates that operation succeeded.
    /// This overloads adds additional information specified in <paramref name="info"/>.
    /// </summary>
    /// <returns></returns>
    public LocalInteractionMessageResponse GetSuccessfulResponse(string info);
}