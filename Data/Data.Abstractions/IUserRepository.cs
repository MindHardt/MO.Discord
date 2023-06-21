using Data.Entities.Users;
using Disqord;

namespace Data.Abstractions;

public interface IUserRepository
{
    public Task<UserData?> GetUserData(Snowflake userId);

    public Task<UserData> UpdateUserData(UserData userData);
}