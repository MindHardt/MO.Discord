using System.Diagnostics.CodeAnalysis;

namespace BotServices.Exceptions;

public class AccessException : Exception
{
    public AccessException(string? message) : base(message)
    {
    }

    public static void ThrowIf([DoesNotReturnIf(true)] bool check, string? message = null)
    {
        if (check)
            throw new AccessException(message ?? "У вас нет прав на это действие");
    }
}