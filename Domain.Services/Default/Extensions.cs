using Data.Entities.Users;
using Disqord;
using Domain.Services.Core;

namespace Domain.Services.Default;

public static class Extensions
{
    /// <summary>
    /// Determines whether user has created all their allowed tags,
    /// </summary>
    /// <param name="service"></param>
    /// <param name="userId"></param>
    /// <returns><see langword="true"/> if user can not create more tags, otherwise <see langword="false"/>.</returns>
    public static async Task<bool> TagsLimitExceeded(this IUserService service, Snowflake userId)
    {
        UserData user = await service.GetOrCreateAsync(userId);
        return await TagsLimitExceeded(service, user);
    }

    /// <summary>
    /// Determines whether user has created all their allowed tags,
    /// </summary>
    /// <param name="service"></param>
    /// <param name="userData"></param>
    /// <returns><see langword="true"/> if user can not create more tags, otherwise <see langword="false"/>.</returns>
    public static async Task<bool> TagsLimitExceeded(this IUserService service, UserData userData)
    {
        var limit = service.GetTagLimit(userData);

        return limit switch
        {
            null => false,
            0 => true,
            _ => await service.GetTagsCountAsync(userData) >= limit
        };
    }
}