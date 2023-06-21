using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Domain.Exceptions;

public class ServiceNotFoundException : ArgumentException
{
    public ServiceNotFoundException(string? message, string? paramName = null) 
        : base(message ?? ExceptionResources.ServiceNotFoundException_DefaultMessage, paramName)
    { }
    
    public static void ThrowIfNull(
        [NotNull] object? argument,
        string? message = null,
        [CallerArgumentExpression("argument")] string? paramName = null)
    {
        if (argument is null)
            throw new ServiceNotFoundException(message, paramName);
    }
}