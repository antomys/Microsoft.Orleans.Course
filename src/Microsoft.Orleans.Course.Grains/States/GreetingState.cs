using Microsoft.Orleans.Course.Grains.Events;

namespace Microsoft.Orleans.Course.Grains.States;

[GenerateSerializer]
internal sealed class GreetingState
{
    [Id(0)]
    public string? Greeting { get; private set; }

    public GreetingState Apply(GreetingEvent greetingEvent)
    {
        Greeting = greetingEvent.Greeting;

        return this;
    }
}