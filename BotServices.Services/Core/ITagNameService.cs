namespace BotServices.Services.Core;

public interface ITagNameService
{
    /// <summary>
    /// Attempts to find tag name in <paramref name="input"/>
    /// </summary>
    /// <returns>the found <see cref="Tag"/> name or <see langword="null"/> if none is found.</returns>
    public string? FindTagName(string input);
    
    /// <summary>
    /// Checks whether <see cref="name"/> is valid.
    /// </summary>
    /// <returns><see langword="true"/> if tag name is valid, otherwise <see langword="false"/>.</returns>
    public bool TagNameValid(string name);
}