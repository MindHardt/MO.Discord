using Domain.Dispatcher.Core;
using Microsoft.Extensions.DependencyInjection;

namespace Domain.Dispatcher.Mappers.Common;

public class MappingProvider : IMappingProvider
{
    private readonly IServiceProvider _servicesProvider;

    public MappingProvider(IServiceProvider servicesProvider)
    {
        _servicesProvider = servicesProvider;
    }

    public IMessageMapper<TSource> GetMessageMapper<TSource>() =>
        _servicesProvider.GetRequiredService<IMessageMapper<TSource>>();

    public ITypeMapper<TSource, TResult> GetTypeMapper<TSource, TResult>() =>
        _servicesProvider.GetRequiredService<ITypeMapper<TSource, TResult>>();
}