namespace Domain.Models;

/// <summary>
/// Represents a stream with name, usually it is used for files.
/// </summary>
/// <param name="Content"></param>
/// <param name="Name"></param>
public record NamedStream(Stream Content, string Name)
{
    public static implicit operator NamedStream(FileStream fs) => new(fs, fs.Name);
}