namespace Domain.Dispatcher.Core;

public interface IExceptionFormatter<out TResult> : IFormatter<Exception, TResult> where TResult : notnull
{ }