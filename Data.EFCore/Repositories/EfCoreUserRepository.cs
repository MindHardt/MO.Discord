using Data.Abstractions;
using Data.Entities.Users;
using Disqord;
using Microsoft.EntityFrameworkCore;

namespace Data.EFCore.Repositories;

public class EfCoreUserRepository :
    EfCoreRepositoryBase<UserData>,
    IUserRepository
{
    public EfCoreUserRepository(DbContext ctx) : base(ctx)
    {
    }

    public async Task<UserData?> GetUserData(Snowflake userId)
    {
        return await Set.FirstOrDefaultAsync(u => u.UserId == userId);
    }

    public async Task<UserData> UpdateUserData(UserData userData)
    {
        var entry = await GetUserData(userData.UserId) is null ? 
            Set.Add(userData) : 
            Set.Update(userData);

        await CommitAsync();
        return entry.Entity;
    }
}