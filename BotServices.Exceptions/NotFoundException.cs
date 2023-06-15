using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace BotServices.Exceptions;

public class NotFoundException : ArgumentException
{
    public NotFoundException(string? message, string? paramName = null) 
        : base(message, paramName)
    {
    }

    public static void ThrowIfNull(
        [NotNull] object? argument,
        [CallerArgumentExpression("argument")] string? paramName = null)
    {
        if (argument is null)
            throw new NotFoundException(null, paramName);
    }
}