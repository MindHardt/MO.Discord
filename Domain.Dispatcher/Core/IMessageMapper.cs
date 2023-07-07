using Disqord;

namespace Domain.Dispatcher.Core;

public interface IMessageMapper<in TSource>
{
    /// <summary>
    /// Maps contents of <typeparamref name="TSource"/> to desired derivative of <see cref="LocalMessageBase"/>.
    /// </summary>
    /// <param name="source"></param>
    /// <typeparam name="TMessage"></typeparam>
    /// <returns>Reference to a new <typeparamref name="TMessage"/>.</returns>
    public TMessage MapAs<TMessage>(TSource source) where TMessage : LocalMessageBase, new();
}