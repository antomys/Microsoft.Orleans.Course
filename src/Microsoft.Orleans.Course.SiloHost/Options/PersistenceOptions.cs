using Microsoft.Orleans.Course.SiloHost.Enums;

namespace Microsoft.Orleans.Course.SiloHost.Options;

public sealed class PersistenceOptions
{
    internal const string SectionName = "Orleans:Persistence";

    public bool IsEnabled { get; init; }

    public PersistenceType? Type { get; init; }
    
    public string? ConnectionString { get; init; }
}