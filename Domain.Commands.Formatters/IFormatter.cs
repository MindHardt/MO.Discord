namespace Domain.Commands.Formatters;

public interface IFormatter
{
    /// <summary>
    /// Formats <paramref name="source"/> and returns result as <see cref="object"/>.
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public object Format(object source);
    
    /// <summary>
    /// Formats <paramref name="source"/> and returns result as <typeparamref name="TResult"/>.
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public TResult Format<TResult>(object source)
        => (TResult)Format(source);
}

public interface IFormatter<in TSource, out TResult> : IFormatter
    where TSource : notnull
    where TResult : notnull
{
    object IFormatter.Format(object source)
        => Format((TSource)source);

    /// <summary>
    /// Formats <paramref name="source"/> as <see cref="TResult"/>.
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    protected TResult Format(TSource source);
}