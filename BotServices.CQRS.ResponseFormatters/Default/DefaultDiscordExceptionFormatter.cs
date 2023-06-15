using BotServices.CQRS.ResponseFormatters.Core;
using BotServices.Exceptions;
using Qmmands;

namespace BotServices.CQRS.ResponseFormatters.Default;

public class DefaultDiscordExceptionFormatter : IDiscordExceptionFormatter
{
    public IResult FormatException(Exception ex)
    {
        var message = ex switch
        {
            NotFoundException nfe => $"Не найдено: {nfe.ParamName}",
            AccessException => $"У вас нет доступа к этому",
            NotImplementedException => "Не найден обработчик комманды",
            _ => ex.Message
        };

        return new ExceptionResult(message, ex);
    }
}