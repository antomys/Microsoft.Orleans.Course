using Orleans.EventSourcing;
using Orleans.Runtime;
using Orleans.Serialization;

namespace Microsoft.Orleans.Course.SiloHost.Options;

/// <summary>
/// Functionality for use by log view adaptors that run distributed protocols.
/// This class allows access to these services to providers that cannot see runtime-internals.
/// It also stores grain-specific information like the grain reference, and caches
/// </summary>
internal sealed class ProtocolServices : ILogConsistencyProtocolServices
{
    private readonly ILogger? _log;
    private readonly DeepCopier _deepCopier;
    private readonly IGrainContext _grainContext;

    public ProtocolServices(
        IGrainContext grainContext,
        ILoggerFactory loggerFactory,
        DeepCopier deepCopier,
        ILocalSiloDetails siloDetails)
    {
        _grainContext = grainContext;
        _log = loggerFactory.CreateLogger<ProtocolServices>();
        _deepCopier = deepCopier;
        MyClusterId = siloDetails.ClusterId;
    }

    public GrainId GrainId => _grainContext.GrainId;

    public string MyClusterId { get; }

    public T DeepCopy<T>(T value) => _deepCopier.Copy(value);

    public void ProtocolError(string msg, bool throwexception)
    {
        _log?.LogError(
            (int)(throwexception ? ErrorCode.LogConsistency_ProtocolFatalError : ErrorCode.LogConsistency_ProtocolError),
            "{GrainId} Protocol Error: {Message}",
            _grainContext.GrainId,
            msg);

        if (!throwexception)
            return;

        throw new OrleansException($"{msg} (grain={_grainContext.GrainId}, cluster={this.MyClusterId})");
    }

    public void CaughtException(string where, Exception e)
    {
        _log?.LogError(
            (int)ErrorCode.LogConsistency_CaughtException,
            e,
            "{GrainId} exception caught at {Location}",
            _grainContext.GrainId,
            where);
    }

    public void CaughtUserCodeException(string callback, string where, Exception e)
    {
        _log?.LogWarning(
            (int)ErrorCode.LogConsistency_UserCodeException,
            e,
            "{GrainId} exception caught in user code for {Callback}, called from {Location}",
            _grainContext.GrainId,
            callback,
            where);
    }

    public void Log(LogLevel level, string format, params object[] args)
    {
        if (_log is null || !_log.IsEnabled(level))
        {
            return;
        }
        
        var msg = $"{_grainContext.GrainId} {string.Format(format, args)}";
        _log.Log(level, 0, msg, null, (m, _) => $"{m}");
    }
}