using Data.Entities.Users;
using Disqord.Bot.Commands;
using Domain.Services.Core;
using Microsoft.Extensions.DependencyInjection;
using Qmmands;

namespace Domain.Bot.Checks;

public class RequireAuthorAccessLevelAttribute : DiscordCheckAttribute
{
    private readonly AccessLevel _minimumLevel;

    public RequireAuthorAccessLevelAttribute(AccessLevel minimumLevel)
    {
        _minimumLevel = minimumLevel;
    }

    public override async ValueTask<IResult> CheckAsync(IDiscordCommandContext context)
    {
        using var scope = context.Services.CreateScope();
        var userService = scope.ServiceProvider.GetRequiredService<IUserService>();

        var user = await userService.GetOrCreateAsync(context.AuthorId);

        return user.AccessLevel >= _minimumLevel 
            ? Results.Success 
            : Results.Failure(string.Format(CheckResources.Failure_AccessLevelTooLow, _minimumLevel.ToString()));
    }
}