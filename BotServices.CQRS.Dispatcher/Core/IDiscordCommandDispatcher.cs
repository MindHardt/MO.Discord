using Qmmands;

namespace BotServices.CQRS.Dispatcher.Core;

public interface IDiscordCommandDispatcher
{
    public Task<IResult> DispatchAsync<TRequest>(TRequest request)
        where TRequest : notnull;
}