using Microsoft.Orleans.Course.Grains.Events;
using Microsoft.Orleans.Course.Grains.Interfaces;
using Microsoft.Orleans.Course.Grains.States;
using Orleans.Providers;

namespace Microsoft.Orleans.Course.Grains.Grains;

[LogConsistencyProvider(ProviderName = "EventSource")]
internal sealed class GreetingSourcedGrain 
    : BaseEventSourcedGrain<GreetingState, GreetingEvent>,
        IGreetingGrain
{
    public async ValueTask<string> Send(string greeting)
    {
        var state = State.Greeting;
        
        RaiseEvent(new GreetingEvent { Greeting = greeting});

        await ConfirmEvents();

        return greeting;
    }

    protected override long GetGrainKey()
    {
        return this.GetPrimaryKeyLong();
    }
}