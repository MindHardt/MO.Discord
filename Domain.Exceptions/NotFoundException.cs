using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Domain.Exceptions;

public class NotFoundException : ArgumentException
{
    public NotFoundException(string? message, string? paramName = null) 
        : base(message ?? ExceptionResources.NotFoundException_DefaultMessage, paramName)
    {
    }

    public static void ThrowIfNull(
        [NotNull] object? argument,
        string? message = null,
        [CallerArgumentExpression("argument")] string? paramName = null)
    {
        if (argument is null)
            throw new NotFoundException(message, paramName);
    }
}