using Data.Entities.Tags;

namespace Domain.Services.Abstractions.Tags;

public interface ITagNameService
{
    /// <summary>
    /// Validates <see cref="Tag"/> name.
    /// </summary>
    /// <param name="name">The checked tag name.</param>
    /// <returns><see langword="true"/> if tag name is valid, otherwise <see langword="false"/>.</returns>
    public bool ValidateName(string name);

    /// <summary>
    /// Attempts to find tag name hidden in <see cref="text"/>.
    /// </summary>
    /// <param name="text">The text to search in.</param>
    /// <param name="prefix">The string prefix that must directly precede the <see cref="ITag"/> name.</param>
    /// <returns>The found <see cref="Tag"/>s name of <see langword="null"/> if none is found.</returns>
    public string? FindTagName(string text, string prefix = "$");
}