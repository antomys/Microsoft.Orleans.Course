namespace Microsoft.Orleans.Course.Grains.Interfaces;

public interface IHello : IGrainWithGuidKey
{
    ValueTask<string> SayHello(string greeting);
}