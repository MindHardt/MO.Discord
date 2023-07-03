using Disqord;

namespace Domain.Factories.Core;

public interface IDiscordResponseFactory
{
    /// <summary>
    /// Gets a default successful message response of type <typeparamref name="TMessage"/>.
    /// </summary>
    /// <param name="additionalInfo"></param>
    /// <typeparam name="TMessage"></typeparam>
    /// <returns></returns>
    public TMessage GetSuccessfulMessageResponse<TMessage>(string? additionalInfo = null)
        where TMessage : LocalMessageBase, new();
    
    /// <summary>
    /// Gets a default failed message response of type <typeparamref name="TMessage"/>.
    /// </summary>
    /// <param name="additionalInfo"></param>
    /// <typeparam name="TMessage"></typeparam>
    /// <returns></returns>
    public TMessage GetFailedMessageResponse<TMessage>(string? additionalInfo = null)
        where TMessage : LocalMessageBase, new();

    /// <summary>
    /// Formats <paramref name="value"/> in a readable way.
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public string GetBoolean(bool value);
}