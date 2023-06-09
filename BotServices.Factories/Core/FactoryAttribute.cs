namespace BotServices.Factories.Core;

/// <summary>
/// Defines that decorated class is a factory and should be injected by <see cref="DependencyInjection"/>.
/// </summary>
[AttributeUsage(AttributeTargets.Class)]
public class FactoryAttribute : Attribute
{ }