namespace Domain.Commands.Formatters;

public interface IAggregateFormatter
{
    /// <summary>
    /// Formats <paramref name="source"/> to the specified <typeparamref name="TResult"/> type.
    /// </summary>
    /// <param name="source"></param>
    /// <typeparam name="TResult"></typeparam>
    /// <returns></returns>
    public TResult Format<TResult>(object source);
}