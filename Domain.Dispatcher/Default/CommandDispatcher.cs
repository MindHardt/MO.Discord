using Domain.Dispatcher.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Domain.Dispatcher.Default;

public class CommandDispatcher : ICommandDispatcher
{
    private readonly IMediator _mediator;
    private readonly IAggregateFormatter _formatter;
    private readonly ILogger<CommandDispatcher> _logger;

    public CommandDispatcher(
        IMediator mediator, 
        IAggregateFormatter formatter, 
        ILogger<CommandDispatcher> logger)
    {
        _mediator = mediator;
        _formatter = formatter;
        _logger = logger;
    }

    public async Task<TResponse> ExecuteAs<TResponse>(object request) where TResponse : notnull
    {
        _logger.LogInformation("Received request: {Request}", request);
        try
        {
            var response = await _mediator.Send(request);
            ArgumentNullException.ThrowIfNull(response);
            
            _logger.LogInformation("Produced response: {Response}", response);
            return _formatter.Format<TResponse>(response);
        }
        catch (Exception ex)
        {
            _logger.LogInformation(ex,"An exception occured when processing request [{Request}]", request);
            return _formatter.FormatException<TResponse>(ex);
        }
    }
}