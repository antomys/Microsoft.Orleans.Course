namespace Microsoft.Orleans.Course.Grains.Events;

[GenerateSerializer]
internal sealed class GreetingEvent
{
    [Id(0)]
    public string? Greeting { get; init; }
}