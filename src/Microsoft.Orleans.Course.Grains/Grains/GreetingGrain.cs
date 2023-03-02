using Microsoft.Orleans.Course.Grains.Events;
using Microsoft.Orleans.Course.Grains.Interfaces;
using Microsoft.Orleans.Course.Grains.States;
using Orleans.EventSourcing;
using Orleans.Providers;

namespace Microsoft.Orleans.Course.Grains.Grains;

[LogConsistencyProvider(ProviderName = "StateStorage")]
internal sealed class GreetingGrain 
    : JournaledGrain<GreetingState, GreetingEvent>,
        IGreetingGrain
{
    public async ValueTask<string> Send(string greeting)
    {
        var state = State.Greeting;
        
        RaiseEvent(new GreetingEvent { Greeting = greeting});

        await ConfirmEvents();

        return greeting;
    }
}