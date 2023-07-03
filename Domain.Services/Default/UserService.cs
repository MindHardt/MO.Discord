using Data.Abstractions;
using Data.Entities.Users;
using Disqord;
using Domain.Services.Core;

namespace Domain.Services.Default;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly ITagRepository _tagRepository;

    public UserService(
        IUserRepository userRepository, 
        ITagRepository tagRepository)
    {
        _userRepository = userRepository;
        _tagRepository = tagRepository;
    }

    public async Task<UserData> GetOrCreateAsync(Snowflake userId)
    {
        return await _userRepository.GetUserData(userId) ?? new UserData() { UserId = userId };
    }

    public int? GetTagLimit(UserData userData) => userData.CustomTagLimit ?? userData.AccessLevel switch
    {
        UserAccessLevel.Default => 5,
        UserAccessLevel.Intermediate => 9,
        UserAccessLevel.Advanced => 15,
        UserAccessLevel.Helper => 50,
        UserAccessLevel.Moderator => 100,
        UserAccessLevel.Admin => null,
        _ => throw new ArgumentOutOfRangeException(nameof(userData))
    };

    public async Task<int> GetTagsCountAsync(UserData user)
    {
        return await _tagRepository.CountTagsOf(user.UserId);
    }
}