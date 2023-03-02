namespace Microsoft.Orleans.Course.Grains.Interfaces;

public interface IHelloGrain : IGrainWithGuidKey
{
    ValueTask<string> SayHello(string greeting);
}