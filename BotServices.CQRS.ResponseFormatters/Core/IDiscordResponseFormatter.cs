using Qmmands;

namespace BotServices.CQRS.ResponseFormatters.Core;

public interface IDiscordResponseFormatter
{
    /// <summary>
    /// Formats <paramref name="response"/> to the discord <see cref="IResult"/>.
    /// </summary>
    /// <param name="response"></param>
    /// <returns></returns>
    public IResult FormatResponse(object response);
}

public interface IDiscordResponseFormatter<in TResponse> : IDiscordResponseFormatter
{
    IResult IDiscordResponseFormatter.FormatResponse(object response)
        => FormatResponse((TResponse)response);

    /// <summary>
    /// Formats strongly-typed<paramref name="response"/>
    /// to the discord <see cref="IResult"/>.
    /// </summary>
    /// <param name="response"></param>
    /// <returns></returns>
    public IResult FormatResponse(TResponse response);
}