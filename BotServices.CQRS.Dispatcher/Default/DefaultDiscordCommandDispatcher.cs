using BotServices.CQRS.Dispatcher.Core;
using BotServices.CQRS.ResponseFormatters.Core;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using Qmmands;

namespace BotServices.CQRS.Dispatcher.Default;

public class DefaultDiscordCommandDispatcher : IDiscordCommandDispatcher
{
    private readonly IServiceProvider _services;
    private readonly IMediator _mediator;
    private readonly IDiscordExceptionFormatter _discordExceptionFormatter;
    private readonly IMemoryCache _memoryCache;

    public DefaultDiscordCommandDispatcher(
        IServiceProvider services, 
        IMediator mediator, 
        IDiscordExceptionFormatter discordExceptionFormatter, 
        IMemoryCache memoryCache)
    {
        _services = services;
        _mediator = mediator;
        _discordExceptionFormatter = discordExceptionFormatter;
        _memoryCache = memoryCache;
    }

    public async Task<IResult> DispatchAsync<TRequest>(TRequest request)
        where TRequest : notnull
    {
        try
        {
            var response = await _mediator.Send(request);
            ArgumentNullException.ThrowIfNull(response);

            IDiscordResponseFormatter? formatter = null;

            var cacheName = GetFormatterCacheName(response.GetType());
            if (_memoryCache.TryGetValue(cacheName, out formatter) is false)
            {
                var formatterBaseType = typeof(IDiscordResponseFormatter<>);
                var formatterType = formatterBaseType.MakeGenericType(response.GetType());
                formatter = _services.GetService(formatterType) as IDiscordResponseFormatter;

                _memoryCache.Set(cacheName, formatter);
            }

            if (formatter is null) throw new NotImplementedException();
            return formatter.FormatResponse(response);
        }
        catch (Exception e)
        {
            return _discordExceptionFormatter.FormatException(e);
        }
    }

    private static string GetFormatterCacheName(Type type)
        => $"FMT_{type.Name}";
}