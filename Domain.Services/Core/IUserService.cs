using Data.Entities.Tags;
using Data.Entities.Users;
using Disqord;

namespace Domain.Services.Core;

public interface IUserService
{
    /// <summary>
    /// Gets the saved <see cref="UserData"/> with specified <paramref name="userId"/>
    /// or creates a new one if none is found.
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    public Task<UserData> GetOrCreateAsync(Snowflake userId);

    /// <summary>
    /// Gets the total amount of <see cref="Tag"/>s that user can own or <see langword="null"/>
    /// If there is no limit.
    /// </summary>
    /// <param name="userData"></param>
    /// <returns></returns>
    public int? GetTagLimit(UserData userData);

    /// <summary>
    /// Gets the total amount of <see cref="Tag"/>s that user owns.
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    public Task<int> GetTagsCountAsync(UserData user);
}