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
        return await _userRepository.FindUser(userId) ?? new UserData { UserId = userId };
    }

    public async ValueTask<UserData> UpdateUserAsync(UserData user)
    {
        return await _userRepository.UpdateUser(user);
    }

    public int? GetTagLimit(UserData userData) => userData.CustomTagLimit ?? userData.AccessLevel switch
    {
        AccessLevel.Default => 5,
        AccessLevel.Intermediate => 9,
        AccessLevel.Advanced => 15,
        AccessLevel.Helper => 50,
        AccessLevel.Moderator => 100,
        AccessLevel.Admin => null,
        _ => throw new ArgumentOutOfRangeException(nameof(userData))
    };

    public async Task<int> GetTagsCountAsync(UserData user)
    {
        return await _tagRepository.CountTagsOf(user.UserId);
    }
}