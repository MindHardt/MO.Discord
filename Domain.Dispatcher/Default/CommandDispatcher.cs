using Domain.Dispatcher.Core;
using MediatR;

namespace Domain.Dispatcher.Default;

public class CommandDispatcher : ICommandDispatcher
{
    private readonly IMediator _mediator;
    private readonly IAggregateFormatter _formatter;

    public CommandDispatcher(
        IMediator mediator, 
        IAggregateFormatter formatter)
    {
        _mediator = mediator;
        _formatter = formatter;
    }

    public async Task<TResponse> ExecuteAs<TResponse>(object request) where TResponse : notnull
    {
        try
        {
            var response = await _mediator.Send(request);
            ArgumentNullException.ThrowIfNull(response);

            return _formatter.Format<TResponse>(response);
        }
        catch (Exception ex)
        {
            return _formatter.FormatException<TResponse>(ex);
        }
    }
}