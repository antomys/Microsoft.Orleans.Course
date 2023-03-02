using Orleans.EventSourcing;
using Orleans.EventSourcing.CustomStorage;

namespace Microsoft.Orleans.Course.Grains.Grains;

internal abstract class BaseEventSourcedGrain<TGrainState, TEventBase>
    : JournaledGrain<TGrainState, TEventBase>, 
        ICustomStorageInterface<TGrainState, TEventBase> 
    where TGrainState : class, new() 
    where TEventBase : class
{

    protected abstract long GetGrainKey();

    public Task<KeyValuePair<int, TGrainState>> ReadStateFromStorage()
    {
        throw new NotImplementedException();
    }

    public Task<bool> ApplyUpdatesToStorage(IReadOnlyList<TEventBase> updates, int expectedversion)
    {
        throw new NotImplementedException();
    }
}