﻿using Domain.Autocompletes.Contexts.Tags;

namespace Domain.Autocompletes.Core;

public interface IAutocompleteProvider
{
    public IAutocomplete<string, TagNameContext> GetTagName();
}