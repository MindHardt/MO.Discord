using System.Text.RegularExpressions;
using BotServices.Entities.Tags;
using BotServices.Services.Core;
using Data.Repositories.Core;
using Disqord;

namespace BotServices.Services.Implementations;

public partial class DefaultTagService : ITagService
{
    private const int MaxTagCount = Discord.Limits.Message.MaxEmbedAmount;
    private const int MaxTagNamesCount = Discord.Limits.ApplicationCommand.Option.MaxChoiceAmount;
    private readonly ITagRepository _repo;

    public DefaultTagService(ITagRepository repo)
    {
        _repo = repo;
    }

    public TagMessage CreateTagMessage(string name, string text, Snowflake ownerId, Snowflake? guildId) => new()
    {
        Name = name,
        Text = text,
        OwnerId = ownerId,
        GuildId = guildId
    };

    public TagAlias CreateTagAlias(TagMessage original, string newName, Snowflake ownerId, Snowflake? guildId) => new()
    {
        ReferencedTag = original as TagMessage ?? 
                        throw new InvalidCastException($"Aliases can only be added to '{nameof(TagMessage)}'"),
        ReferencedTagId = original.Id,
        Name = newName,
        OwnerId = ownerId,
        GuildId = guildId
    };

    public async Task SaveTagAsync(Tag tag, Snowflake authorId, bool authorAdmin)
    {
        if (tag.Id != default && authorAdmin is false)
        {
            Tag? dbTag = await _repo.GetTagAsync(tag.Name);

            if (dbTag is not null && dbTag.OwnerId != authorId)
                throw new InvalidOperationException("You are not allowed to edit this tag.");
        }

        await _repo.SaveTagAsync(tag);
    }

    public Task<IReadOnlyCollection<Tag>> GetTagsAsync(Snowflake? guildId, string? prompt)
    {
        return _repo.GetTagsAsync(MaxTagCount, guildId, prompt);
    }

    public async Task<Tag?> GetTagAsync(string name, Snowflake? guildId)
    {
        var tag = await _repo.GetTagAsync(name);

        if (tag?.IsPublic is false && tag.GuildId != guildId)
            return null;
            
        return tag;
    }

    public Task<IReadOnlyCollection<string>> GetTagNames(Snowflake? guildId, string prompt)
    {
        return _repo.GetTagNames(MaxTagNamesCount, guildId, prompt);
    }

    [GeneratedRegex(@"\$(?<NAME>[\w\d]+)")]
    public partial Regex GetTagNameRegex();

    public TMessage CreateMessage<TMessage>(Tag tag) where TMessage : LocalMessageBase, new()
    {
        return new TMessage().WithContent(tag.Content);
    }
    
    public string CreateOverview(Tag tag)
    {
        return $"{GetPublicMark(tag)}{GetTypeMark(tag)} | {tag.Name}";
    }

    private static string GetPublicMark(Tag tag) => tag.IsPublic ? "🔓" : "🔒";
    private static string GetTypeMark(Tag tag) => tag is TagMessage ? "✉️" : "🔗";
}