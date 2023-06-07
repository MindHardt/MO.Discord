using BotServices.Entities.GuildData;
using BotServices.Services.Core;
using Disqord.Bot.Commands;
using Disqord.Bot.Commands.Application;
using Qmmands;

namespace BotServices.Commands.Slash;

[SlashGroup("owner")]
[RequireBotOwner]
public class OwnerCommands : DiscordApplicationGuildModuleBase
{
    private readonly IGuildDataService _guildDataService;
    private readonly IResponseService _responseService;

    public OwnerCommands(
        IGuildDataService guildDataService, 
        IResponseService responseService)
    {
        _guildDataService = guildDataService;
        _responseService = responseService;
    }
    
    [SlashCommand("inline-tags")]
    [Description("Переключает поиск инлайн-тегов для сервера")]
    public async ValueTask<IResult> UpdateInlineTags(
        [Name("enabled")] bool enabled)
    {
        var guildData = await _guildDataService.GetGuildDataAsync(Context.GuildId);
        guildData ??= new GuildData { GuildId = Context.GuildId };

        guildData.InlineTagsEnabled = enabled;

        await _guildDataService.SaveGuildDataAsync(guildData);

        string enabledStatus = enabled ? "🟢" : "⭕";
        var response = _responseService.GetSuccessfulResponse($"Поиск тегов на сервере - {enabledStatus}");
        return Response(response);
    }
}