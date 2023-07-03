using Domain.Dispatcher.Core;
using Domain.Exceptions;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace Domain.Dispatcher.Default;

/// <summary>
/// A default implementation of <see cref="IAggregateFormatter"/>
/// that utilizes injected <see cref="IServiceProvider"/> to find <see cref="IFormatter"/> implementations
/// and caches found instances with <see cref="IMemoryCache"/>.
/// </summary>
public class AggregateFormatter : IAggregateFormatter
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<AggregateFormatter> _logger;

    public AggregateFormatter(
        IServiceProvider serviceProvider, 
        ILogger<AggregateFormatter> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    public TResult Format<TResult>(object source)
    {
        Type from = source.GetType();
        Type to = typeof(TResult);
        Type formatterType = typeof(IFormatter<,>).MakeGenericType(from, to);

        IFormatter formatter = GetFormatter(formatterType);

        return formatter.Format<TResult>(source);
    }

    public TResult FormatException<TResult>(Exception exception) where TResult : notnull
    {
        IFormatter formatter = GetFormatter(typeof(IExceptionFormatter<TResult>));
        return formatter.Format<TResult>(exception);
    }

    private IFormatter GetFormatter(Type formatterType)
    {
        _logger.LogInformation("Attempting to get formatter [{Type}]", 
            formatterType.Name);
        
        var formatter = _serviceProvider.GetService(formatterType) as IFormatter;
        
        ServiceNotFoundException.ThrowIfNull(formatter);
        
        _logger.LogInformation("Got formatter [{Type} <=> {Formatter}]", 
            formatterType.Name, formatter.GetType().Name);
        
        return formatter;
    }
}