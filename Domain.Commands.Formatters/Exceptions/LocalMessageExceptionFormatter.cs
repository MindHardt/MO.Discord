using System.Text;
using Disqord;
using Domain.Factories.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Domain.Commands.Formatters.Exceptions;

public class LocalMessageExceptionFormatter:
    IExceptionFormatter<LocalInteractionMessageResponse>
{
    private readonly IDiscordResponseFactory _discordResponseFactory;

    public LocalMessageExceptionFormatter(IDiscordResponseFactory discordResponseFactory)
    {
        _discordResponseFactory = discordResponseFactory;
    }

    public LocalInteractionMessageResponse Format(Exception exception)
        => _discordResponseFactory.GetFailedMessageResponse<LocalInteractionMessageResponse>(GetDescription(exception));

    private string GetDescription(Exception exception)
    {
        var rawMessage = new StringBuilder(exception switch
        {
            DbUpdateException => $"Ошибка базы данных",
            ArgumentException => $"Ошибка ввода данных",
            _ => $"Неизвестная ошибка"
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