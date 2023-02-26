namespace Microsoft.Orleans.Course.SiloHost.Extensions;

internal static partial class LoggingExtensions
{
    [LoggerMessage(
        Level = LogLevel.Information,
        Message = "Invoking interface {InterfaceTypeValue} with method {ContextMethodName}")]
    public static partial void LogFilter(
        this ILogger logger,
        string interfaceTypeValue,
        string contextMethodName);
}