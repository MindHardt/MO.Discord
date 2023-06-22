namespace Domain.Commands.Formatters;

public interface IExceptionFormatter<out TResult> : IFormatter<Exception, TResult> where TResult : notnull
{ }