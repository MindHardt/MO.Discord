using Disqord.Bot.Commands;
using Domain.Services.Core;
using Microsoft.Extensions.DependencyInjection;
using Qmmands;

namespace Domain.Bot.Checks;

public class RequireAdultAllowedGuild : DiscordCheckAttribute
{
    private readonly bool _allowDMs;

    public RequireAdultAllowedGuild(bool allowDMs = false)
    {
        _allowDMs = allowDMs;
    }

    public override async ValueTask<IResult> CheckAsync(IDiscordCommandContext context)
    {
        if (_allowDMs) return Results.Success;

        using var scope = context.Services.CreateScope();
        var userService = scope.ServiceProvider.GetRequiredService<IGuildService>();

        var guildId = context.GuildId;
        if (guildId is null)
        {
            return Results.Failure(CheckResources.Failure_GuildRequired);
        }
        var guild = await userService.GetOrCreateAsync(guildId.Value);

        return guild.AdultAllowed
            ? Results.Success 
            : Results.Failure(CheckResources.Failure_AdultNotAllowed);
    }
}