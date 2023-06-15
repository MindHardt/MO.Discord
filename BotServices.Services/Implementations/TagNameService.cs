using System.Text.RegularExpressions;
using BotServices.Services.Core;

namespace BotServices.Services.Implementations;

public partial class TagNameService : ITagNameService
{
    public string? FindTagName(string input)
    {
        Match match = TagSearchRegex().Match(input);
        return match.Success ? match.Groups["NAME"].Value : null;
    }

    public bool TagNameValid(string name)
    {
        return TagNameRegex().IsMatch(name);
    }
    
    [GeneratedRegex(@"\$(?<NAME>[\d\p{L}-_]+)")]
    public partial Regex TagSearchRegex();
    [GeneratedRegex(@"^[\d\p{L}-_]+$")]
    public partial Regex TagNameRegex();
}