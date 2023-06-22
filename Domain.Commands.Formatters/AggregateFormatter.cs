using Domain.Exceptions;
using Microsoft.Extensions.Caching.Memory;

namespace Domain.Commands.Formatters;

/// <summary>
/// A default implementation of <see cref="IAggregateFormatter"/>
/// that utilizes injected <see cref="IServiceProvider"/> to find <see cref="IFormatter"/> implementations
/// and caches found instances with <see cref="IMemoryCache"/>.
/// </summary>
public class AggregateFormatter : IAggregateFormatter
{
    private readonly IServiceProvider _serviceProvider;

    public AggregateFormatter(
        IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public TResult Format<TResult>(object source)
    {
        Type formatterType = typeof(IFormatter<,>).MakeGenericType(source.GetType(), typeof(TResult));
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
        var formatter = _serviceProvider.GetService(formatterType) as IFormatter;
        
        ServiceNotFoundException.ThrowIfNull(formatter);
        
        return formatter;
    }
}