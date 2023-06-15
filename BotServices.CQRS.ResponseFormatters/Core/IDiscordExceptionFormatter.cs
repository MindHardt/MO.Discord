using Qmmands;

namespace BotServices.CQRS.ResponseFormatters.Core;

public interface IDiscordExceptionFormatter
{
    public IResult FormatException(Exception ex);
}