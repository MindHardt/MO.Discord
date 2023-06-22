﻿using Disqord.Bot.Commands.Application;

namespace Domain.Autocompletes.Core;

public interface IAutocomplete<T, in TContext> where T : notnull
{
    public ValueTask Complete(AutoComplete<T> member, TContext context);
}