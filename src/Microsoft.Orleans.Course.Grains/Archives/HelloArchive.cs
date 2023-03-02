namespace Microsoft.Orleans.Course.Grains.Archives;

internal sealed class HelloArchive
{
    public List<string> Greetings { get; } = new();
}