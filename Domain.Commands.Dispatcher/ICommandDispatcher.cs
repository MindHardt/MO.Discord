namespace Domain.Commands.Dispatcher;

public interface ICommandDispatcher
{
    /// <summary>
    /// Attempts to execute <see cref="request"/> and format its response as
    /// <typeparamref name="TResponse"/>.
    /// </summary>
    /// <param name="request"></param>
    /// <typeparam name="TResponse"></typeparam>
    /// <returns></returns>
    public Task<TResponse> ExecuteAs<TResponse>(object request)
        where TResponse : notnull;
}