namespace Data.Entities.Users;

public static class Extensions
{
    /// <summary>
    /// Formats <paramref name="accessLevel"/> in a readable way.
    /// </summary>
    /// <param name="accessLevel"></param>
    /// <returns></returns>
    public static string ToReadable(this AccessLevel accessLevel) => $"[{(int)accessLevel}] {accessLevel}";
}