namespace Domain.Dispatcher.Core;

public interface ITypeMapper<in TSource, out TResult>
{
    /// <summary>
    /// Maps <paramref name="source"/> to the <typeparamref name="TResult"/> object.
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public TResult Map(TSource source);
}