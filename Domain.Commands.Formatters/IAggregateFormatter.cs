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

    /// <summary>
    /// Formats <paramref name="exception"/> to the specified <typeparamref name="TResult"/> type.
    /// </summary>
    /// <param name="exception"></param>
    /// <typeparam name="TResult"></typeparam>
    /// <returns></returns>
    public TResult FormatException<TResult>(Exception exception)
        where TResult : notnull;
}