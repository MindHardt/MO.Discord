using Disqord;

namespace Domain.Dispatcher.Core;

/// <summary>
/// A core mapping provider that aggregates multiple use-case mappers and can provide them.
/// </summary>
public interface IMappingProvider
{
    /// <summary>
    /// Gets a mapper that can transform <typeparamref name="TSource"/> to inheritors of <see cref="LocalMessageBase"/>.
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <returns></returns>
    public IMessageMapper<TSource> GetMessageMapper<TSource>();
    
    /// <summary>
    /// Gets a mapper that can transform <typeparamref name="TSource"/> to <typeparamref name="TResult"/>.
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <returns></returns>
    public ITypeMapper<TSource, TResult> GetTypeMapper<TSource, TResult>();
}