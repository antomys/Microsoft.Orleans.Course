using System.Text.Json;
using Microsoft.Orleans.Course.Grains.Interfaces;
using Microsoft.Orleans.Course.SiloHost.Extensions;
using Orleans.Runtime;

namespace Microsoft.Orleans.Course.SiloHost.Filters;

internal sealed class LoggingFilter : IIncomingGrainCallFilter
{
    private static readonly GrainInterfaceType[] LoggingTypes = 
    {
        GrainInterfaceType.Create(typeof(IHello).FullName!),
    };

    private readonly ILogger<LoggingFilter> _logger;

    public LoggingFilter(ILogger<LoggingFilter> logger)
    {
        _logger = logger;
    }

    public async Task Invoke(IIncomingGrainCallContext context)
    {
        try
        {
            if (LoggingTypes.Contains(context.InterfaceType))
            {
                _logger.LogFilter(context.InterfaceType.ToString()!, context.MethodName);
            }

            await context.Invoke();
        }
        catch (Exception exception)
        {
            using var request = await context.Request.Invoke();
            
            _logger.LogException(
                exception,
                context.InterfaceType.ToString()!,
                context.MethodName, 
                JsonSerializer.Serialize(request.Result));
        }
    }
}