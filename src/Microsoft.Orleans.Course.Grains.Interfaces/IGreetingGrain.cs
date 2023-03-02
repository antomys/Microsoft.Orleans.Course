namespace Microsoft.Orleans.Course.Grains.Interfaces;

public interface IGreetingGrain : IGrainWithIntegerKey
{
    ValueTask<string> Send(string greeting);
}