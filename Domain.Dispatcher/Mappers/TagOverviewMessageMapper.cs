using Data.Entities.Tags;
using Disqord;
using Domain.Dispatcher.Core;
using Domain.Dispatcher.Mappers.Common;
using Domain.Factories.Core;

namespace Domain.Dispatcher.Mappers;

public class TagOverviewMessageMapper : IMessageMapper<TagOverview>
{
    private readonly IDiscordResponseFactory _discordResponseFactory;

    public TagOverviewMessageMapper(IDiscordResponseFactory discordResponseFactory)
    {
        _discordResponseFactory = discordResponseFactory;
    }

    public TMessage MapAs<TMessage>(TagOverview source) where TMessage : LocalMessageBase, new()
    {
        var embedFields = new[]
        {
            new LocalEmbedField()
                .WithName(MappingResources.Tag_Type)
                .WithValue(Markdown.Code(source.Type))
                .WithIsInline(),
            new LocalEmbedField()
                .WithName(MappingResources.Tag_GuildId)
                .WithValue(Markdown.Code(source.GuildId))
                .WithIsInline(),
            new LocalEmbedField()
                .WithName(MappingResources.Tag_OwnerId)
                .WithValue(Markdown.Code(source.OwnerId))
                .WithIsInline(),
            new LocalEmbedField()
                .WithName(MappingResources.Tag_ReferencedTagName)
                .WithValue(Markdown.Code(source.ReferencedTagName ?? "-"))
                .WithIsInline(),
        };
        var title = string.Format(MappingResources.Tag_Name, source.Name);

        return _discordResponseFactory.GetSuccessfulMessageResponse<TMessage>()
            .AddEmbed(new LocalEmbed()
                .WithTitle(title)
                .WithFields(embedFields));
    }
}