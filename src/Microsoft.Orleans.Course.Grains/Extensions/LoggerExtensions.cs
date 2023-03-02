using Microsoft.Extensions.Logging;

namespace Microsoft.Orleans.Course.Grains.Extensions;

internal static partial class LoggerExtensions
{
    [LoggerMessage(
        EventId = 0,
        Level = LogLevel.Information,
        Message = "Message received: {Message}")]
    public static partial void LogSay(this ILogger logger, string message);
    
    [LoggerMessage(
        EventId = 1,
        Level = LogLevel.Information,
        Message = "TraceId received: {TraceId} for grain: {GrainId}")]
    public static partial void LogTracing(this ILogger logger, string traceId, string grainId);
}