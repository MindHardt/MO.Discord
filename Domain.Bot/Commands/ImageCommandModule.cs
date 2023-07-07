using System.Diagnostics.CodeAnalysis;
using Disqord;
using Disqord.Bot.Commands;
using Disqord.Bot.Commands.Application;
using Domain.Bot.Checks;
using Domain.Dispatcher.Core;
using Domain.Dispatcher.Requests.Images;
using Domain.Dispatcher.Responses.Images;
using Domain.Models;
using MediatR;
using Qmmands;
using Color = System.Drawing.Color;

namespace Domain.Bot.Commands;

[SlashGroup("картинка")]
public class ImageCommandModule : DiscordApplicationModuleBase
{
    [StringSyntax(StringSyntaxAttribute.Regex)]
    private const string ColorRegex = @"#?[a-fA-F0-9]{6}";

    private readonly IMediator _mediator;
    private readonly IMappingProvider _mappingProvider;

    public ImageCommandModule(
        IMediator mediator, 
        IMappingProvider mappingProvider)
    {
        _mediator = mediator;
        _mappingProvider = mappingProvider;
    }

    [SlashCommand("цвет")]
    [Description("Показывает картинку определенного цвета")]
    public async ValueTask<IResult> GetColor(
        [Name("hex"), Description("Шестнадцатеричное представление цвета, например #c0ffee или #3aebca")]
        [Regex(ColorRegex)]
        string color)
    {
        color = color.TrimStart('#');
        var request = new GetColorRequest
        {
            Color = Color.FromArgb(Convert.ToInt32(color, fromBase: 16))
        };
        var response = await _mediator.Send(request);
        var mapper = _mappingProvider.GetMessageMapper<NamedStream>();
        return Response(mapper.MapAs<LocalInteractionMessageResponse>(response.Image).WithContent(Markdown.Code($"#{color}")));
    }
    
    [SlashCommand("харам")]
    [Description("Побойся бога. Доступно только на подтвержденных серверах")]
    [RequireAdultAllowedGuild]
    public async ValueTask<IResult> GetDickPic()
    {
        return Results.Failure("NOT IMPLEMENTED");
    }
}