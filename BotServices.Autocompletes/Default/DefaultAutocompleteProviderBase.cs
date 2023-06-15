using BotServices.Autocompletes.Core;
using Microsoft.Extensions.DependencyInjection;
using Qmmands;

namespace BotServices.Autocompletes.Default;

/// <summary>
/// A default implementation of <see cref="IAutocompleteProvider{T,TContext}"/> that forwards
/// abstract <see cref="IAutocomplete{T,TContext}"/> to concrete <typeparamref name="TAutocomplete"/>.
/// </summary>
/// <typeparam name="T"></typeparam>
/// <typeparam name="TContext"></typeparam>
/// <typeparam name="TAutocomplete"></typeparam>
public abstract class DefaultAutocompleteProviderBase<T, TContext, TAutocomplete>
    : IAutocompleteProvider<T, TContext> 
    where T : notnull
    where TContext : ICommandContext
    where TAutocomplete : IAutocomplete<T, TContext>
{
    private readonly IServiceProvider _services;

    protected DefaultAutocompleteProviderBase(IServiceProvider services)
    {
        _services = services;
    }

    public IAutocomplete<T, TContext> GetAutocomplete()
        => _services.GetRequiredService<TAutocomplete>();
}