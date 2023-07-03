using System.Text;
using Disqord;
using Domain.Dispatcher.Core;
using Domain.Factories.Core;
using Microsoft.EntityFrameworkCore;

namespace Domain.Dispatcher.Formatters.Exception;

public class LocalMessageExceptionFormatter:
    IExceptionFormatter<LocalInteractionMessageResponse>
{
    private readonly IDiscordResponseFactory _discordResponseFactory;

    public LocalMessageExceptionFormatter(IDiscordResponseFactory discordResponseFactory)
    {
        _discordResponseFactory = discordResponseFactory;
    }

    public LocalInteractionMessageResponse Format(System.Exception exception)
        => _discordResponseFactory.GetFailedMessageResponse<LocalInteractionMessageResponse>(GetDescription(exception));

    private string GetDescription(System.Exception exception)
    {
        var rawMessage = new StringBuilder(exception switch
        {
            DbUpdateException => "Ошибка базы данных",
            ArgumentException => "Ошибка ввода данных",
            _ => "Неизвестная ошибка"
        });
        rawMessage.Append($"```{exception.Message}");
        
        const int maxMessageLength = Discord.Limits.Message.Embed.MaxDescriptionLength - 3;
        var exceededLength = rawMessage.Length - maxMessageLength;
        if (exceededLength > 0)
        {
            rawMessage.Remove(maxMessageLength, exceededLength);
        }

        return rawMessage.Append("```").ToString();
    }
}