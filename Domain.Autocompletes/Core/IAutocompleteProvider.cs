using Domain.Autocompletes.Contexts.Tags;

namespace Domain.Autocompletes.Core;

public interface IAutocompleteProvider
{
    /// <summary>
    /// Gets <see cref="IAutocomplete{T,TContext}"/> that pastes tag names.
    /// </summary>
    /// <returns></returns>
    public IAutocomplete<string, TagNameContext> GetTagName();
}