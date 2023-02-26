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
    
    [LoggerMessage(
        Level = LogLevel.Error,
        Message = "Error occured during invoking interface {InterfaceTypeValue} with method {ContextMethodName} and response {Response}")]
    public static partial void LogException(
        this ILogger logger,
        Exception exception,
        string interfaceTypeValue,
        string contextMethodName,
        string response);
}