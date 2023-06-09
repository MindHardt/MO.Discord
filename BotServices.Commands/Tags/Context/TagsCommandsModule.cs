using BotServices.Factories.Core;
using BotServices.Services.Core;
using Disqord;
using Disqord.Bot.Commands.Application;
using Disqord.Extensions.Interactivity;
using Disqord.Rest;

namespace BotServices.Commands.Tags.Context;

public class TagsCommandsModule : DiscordApplicationGuildModuleBase
{
    private readonly IDiscordResponseFactory _discordResponseFactory;
    private readonly ITagFactory _tagFactory;
    private readonly ITagService _tagService;

    public TagsCommandsModule(
        IDiscordResponseFactory discordResponseFactory, 
        ITagFactory tagFactory, 
        ITagService tagService)
    {
        _discordResponseFactory = discordResponseFactory;
        _tagFactory = tagFactory;
        _tagService = tagService;
    }

    [MessageCommand("Создать тег")]
    public async ValueTask TagCreate(IMessage message)
    {
        var modalId = Guid.NewGuid().ToString();
        var modal = _discordResponseFactory.GetTagCreationModal(modalId);
        
        // TODO: Сделать модал-команду вместо парсинга модала тут.
        await Context.Interaction.Response().SendModalAsync(modal);
        var modalResult = 
            await Bot.GetInteractivity().WaitForInteractionAsync<TransientModalSubmitInteraction>(Context.ChannelId,
                m => m.CustomId == modalId);

        if (modalResult is null) return;
        
        var row = modalResult.Components[0] as IRowComponent;
        var textInput = row?.Components[0] as ITextInputComponent;
        var tagName = textInput?.Value;

        LocalInteractionMessageResponse response;
        if (tagName is null)
        {
            response = _discordResponseFactory.GetFailedResponse("Не получилось определить название тега");
            await modalResult.Response().SendMessageAsync(response);
            return;
        }

        var tag = _tagFactory.CreateTagMessage(tagName, message, Context.AuthorId, Context.GuildId);
        await _tagService.SaveTagAsync(tag, Context.AuthorId, await Bot.IsOwnerAsync(Context.AuthorId));

        response = _discordResponseFactory.GetSuccessfulResponse($"Успешно создал тег `{tag.Name}`");
        await modalResult.Response().SendMessageAsync(response);
    }   
}