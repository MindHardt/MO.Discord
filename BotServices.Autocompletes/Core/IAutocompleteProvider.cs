using Qmmands;

namespace BotServices.Autocompletes.Core;

public interface IAutocompleteProvider<T, in TContext> 
    where T : notnull
    where TContext : ICommandContext
{
    /// <summary>
    /// Gets the <see cref="IAutocomplete{T,TContext}"/> for current circumstances.
    /// </summary>
    /// <returns></returns>
    public IAutocomplete<T, TContext> GetAutocomplete();
}