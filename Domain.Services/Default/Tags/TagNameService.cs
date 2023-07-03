using System.Text.RegularExpressions;
using Domain.Services.Core.Tags;
using Microsoft.Extensions.Caching.Memory;

namespace Domain.Services.Default.Tags;

public partial class TagNameService : ITagNameService
{
    private readonly IMemoryCache _cache;

    public TagNameService(IMemoryCache cache)
    {
        _cache = cache;
    }

    public bool ValidateName(string name)
        => TagNameRegex().IsMatch(name);

    public string? FindTagName(string text, string prefix = "$")
    {
        var name = GetFinderRegex(prefix).Match(text).Groups["NAME"].Value;
        return string.IsNullOrWhiteSpace(name) ? null : name;
    }
    
    [GeneratedRegex(@"^[\d\p{L}-_]+$")]
    public partial Regex TagNameRegex();

    private Regex GetFinderRegex(string prefix)
    {
        string cacheName = GetFinderCacheName(prefix);
        var cachedRegex = _cache.Get<Regex>(cacheName);
        
        if (cachedRegex is not null) 
            return cachedRegex;
        
        cachedRegex = GenerateFinderRegex(prefix);
        _cache.Set(cacheName, cachedRegex);

        return cachedRegex;
    }
    
    private string GetFinderCacheName(string prefix) => $"{GetType().Name}_V_{prefix}";

    private Regex GenerateFinderRegex(string prefix)
    {
        string regexString = Regex.Escape(prefix) + @"(?<NAME>[\d\p{L}-_]+)";
        return new Regex(regexString, RegexOptions.Compiled);
    }
}