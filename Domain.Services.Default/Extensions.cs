using Data.Entities.Users;
using Disqord;
using Domain.Services.Abstractions;

namespace Domain.Services.Default;

public static class Extensions
{
    public static async Task<bool> TagsLimitExceeded(this IUserService service, Snowflake userId)
    {
        UserData user = await service.GetOrCreateAsync(userId);
        return await TagsLimitExceeded(service, user);
    }

    public static async Task<bool> TagsLimitExceeded(this IUserService service, UserData userData)
    {
        var limit = service.GetTagLimit(userData);
        if (limit is null or 0) return false;
        
        var count = await service.GetTagsCountAsync(userData);
        return count >= limit;
    }
}