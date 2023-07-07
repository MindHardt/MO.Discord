using Data.Entities.Users;
using Disqord;

namespace Data.Abstractions;

public interface IUserRepository
{
    /// <summary>
    /// Attempts to find <see cref="UserData"/> that correlates with <paramref name="userId"/>.
    /// </summary>
    /// <param name="userId"></param>
    /// <returns>The found <see cref="UserData"/> or <see langword="null"/> if none is found.</returns>
    public ValueTask<UserData?> FindUser(Snowflake userId);

    /// <summary>
    /// Saves <paramref name="userData"/> to the storage.
    /// </summary>
    /// <param name="userData"></param>
    /// <returns></returns>
    public ValueTask<UserData> UpdateUser(UserData userData);
}