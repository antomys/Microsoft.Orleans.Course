using Microsoft.Extensions.Logging;

namespace Microsoft.Orleans.Course.Grains.Extensions;

internal static partial class LoggerExtensions
{
    [LoggerMessage(
        Level = LogLevel.Information,
        Message = "Message received: {Message}")]
    public static partial void LogSay(this ILogger logger, string message);
}