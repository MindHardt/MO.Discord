using Domain.Autocompletes.Contexts.Tags;
using Domain.Autocompletes.Core;
using Domain.Autocompletes.Default.Tags;
using Microsoft.Extensions.DependencyInjection;

namespace Domain.Autocompletes.Default;

public class AutocompleteProvider : IAutocompleteProvider
{
    private readonly IServiceProvider _serviceProvider;

    public AutocompleteProvider(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public IAutocomplete<string, TagNameContext> GetTagName() =>
        _serviceProvider.GetRequiredService<TagNameAutocomplete>();
}