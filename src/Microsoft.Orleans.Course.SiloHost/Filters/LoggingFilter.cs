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

    public Task Invoke(IIncomingGrainCallContext context)
    {
        if (LoggingTypes.Contains(context.InterfaceType))
        {
            _logger.LogFilter(context.InterfaceType.ToString()!, context.MethodName);
        }

        return context.Invoke();
    }
}